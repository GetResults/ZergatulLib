﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zergatul.Network.ASN1;

namespace Zergatul.Cryptography.Certificates
{
    /// <summary>
    /// https://tools.ietf.org/html/rfc5280#section-4.2.1.6
    /// </summary>
    public class SubjectAlternativeName : X509Extension
    {
        public GeneralNames Names { get; private set; }

        protected override void Parse(OctetString data)
        {
            var element = ASN1Element.ReadFrom(data.Raw);
            Names = new GeneralNames(element);
        }
    }
}