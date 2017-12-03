﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zergatul.Cryptography.Asymmetric;
using Zergatul.Cryptography.Certificate.Extensions;
using Zergatul.Cryptography.Hash;
using Zergatul.Network;

namespace Zergatul.Cryptography.Certificate
{
    // TODO: handle case when there are 2 certificates with the same subj and auth
    public class X509Tree
    {
        public LinkedCertificate Root { get; private set; }
        public IEnumerable<X509Certificate> Leaves => Root.GetLeaves().Select(lc => lc.Certificate);

        private IRootCertificateStore _store;

        public static X509Tree Build(IEnumerable<X509Certificate> certificates, IRootCertificateStore store = null)
        {
            if (certificates.Count() == 1 && certificates.First().IsSelfSigned())
            {
                // self-signed certificate
                return new X509Tree
                {
                    Root = new LinkedCertificate { Certificate = certificates.First() },
                    _store = store
                };
            }

            if (certificates.Any(c => c.Extensions.OfType<AuthorityKeyIdentifier>().Count() > 1))
                throw new NotSupportedException();
            if (certificates.Any(c => c.Extensions.OfType<SubjectKeyIdentifier>().Count() > 1))
                throw new NotSupportedException();

            if (certificates.Count(c => c.IsSelfSigned())  > 1)
                throw new InvalidOperationException("There can be only 1 self signed certificate in chain");

            var list = certificates.ToList();
            var root = new LinkedCertificate
            {
                Certificate = certificates.Single(c => c.IsSelfSigned())
            };

            list.Remove(root.Certificate);

            while (list.Count > 0)
            {
                bool change = false;
                for (int i = 0; i < list.Count; i++)
                {
                    byte[] authorityKey = list[i].Extensions.Get<AuthorityKeyIdentifier>().KeyIdentifier;
                    var lc = root.FindBySubjectKey(authorityKey);
                    if (lc != null)
                    {
                        var lcnew = new LinkedCertificate
                        {
                            Certificate = list[i],
                            Parent = lc
                        };
                        lc.Children.Add(lcnew);
                        list.RemoveAt(i);
                        i--;
                        change = true;
                    }
                }

                if (!change)
                    throw new InvalidOperationException("Cannot create certificate tree");
            }

            return new X509Tree
            {
                Root = root,
                _store = store
            };
        }

        public bool Validate(X509Certificate certificate)
        {
            var lc = Root.GetLinked(certificate);
            if (lc == null)
                throw new InvalidOperationException("Certificate doesn't belont to tree");

            while (lc.Parent != null)
            {
                var signed = lc.Certificate;
                var parent = lc.Parent.Certificate;

                if (signed.SignatureAlgorithm == OID.ISO.MemberBody.US.RSADSI.PKCS.PKCS1.SHA256WithRSA)
                {
                    var hash = new SHA256();
                    hash.Update(signed.SignedData);
                    var data = hash.ComputeHash();

                    var rsa = parent.PublicKey.ResolveAlgorithm() as RSA;
                    var scheme = rsa.Signature.GetScheme("EMSA-PKCS1-v1.5");
                    scheme.SetParameter(hash.OID);
                    if (!scheme.Verify(signed.Signature, data))
                        return false;
                }
                else
                    throw new NotImplementedException();

                lc = lc.Parent;
            }

            if (_store != null)
                return _store.FindBySubjectKeyId(lc.Certificate.Extensions.Get<SubjectKeyIdentifier>().KeyIdentifier) != null;
            else
                return false;
        }

        public class LinkedCertificate
        {
            public X509Certificate Certificate;
            public LinkedCertificate Parent;
            public List<LinkedCertificate> Children = new List<LinkedCertificate>();

            public LinkedCertificate FindBySubjectKey(byte[] subjectKey)
            {
                byte[] currentKey = Certificate.Extensions.Get<SubjectKeyIdentifier>()?.KeyIdentifier;
                if (currentKey != null && ByteArray.Equals(currentKey, subjectKey))
                    return this;

                foreach (var child in Children)
                {
                    var lc = child.FindBySubjectKey(subjectKey);
                    if (lc != null)
                        return lc;
                }

                return null;
            }

            public IEnumerable<LinkedCertificate> GetLeaves()
            {
                if (Children.Count == 0)
                    yield return this;
                foreach (var child in Children)
                {
                    foreach (var leaf in child.GetLeaves())
                        yield return leaf;
                }
            }

            public LinkedCertificate GetLinked(X509Certificate certificate)
            {
                if (Certificate == certificate)
                    return this;

                foreach (var child in Children)
                {
                    var lc = child.GetLinked(certificate);
                    if (lc != null)
                        return lc;
                }

                return null;
            }
        }
    }
}