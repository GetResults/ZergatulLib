﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zergatul.Network.ASN1;

namespace Zergatul.Cryptography.Certificates
{
    /// <summary>
    /// https://tools.ietf.org/html/rfc5280#section-4.2.1.2
    /// </summary>
    public class SubjectKeyIdentifier : X509Extension
    {
        public byte[] KeyIdentifier { get; private set; }

        protected override void Parse(OctetString data)
        {
            var element = ASN1Element.ReadFrom(data.Raw);

            var os = element as OctetString;
            CertificateParseException.ThrowIfNull(os);

            KeyIdentifier = os.Raw;
        }
    }
}