﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zergatul.Network;
using Zergatul.Network.ASN1;

namespace Zergatul.Network.ASN1.Structures
{
    /// <summary>
    /// https://tools.ietf.org/html/rfc5280#section-4.1.1.2
    /// </summary>
    public class AlgorithmIdentifier
    {
        public OID Algorithm { get; private set; }
        public ASN1Element Parameters { get; private set; }

        public static AlgorithmIdentifier Parse(ASN1Element element)
        {
            var seq = element as Sequence;
            ParseException.ThrowIfNull(seq);
            ParseException.ThrowIfNotEqual(seq.Elements.Count, 2);

            var oi = seq.Elements[0] as ObjectIdentifier;
            ParseException.ThrowIfNull(oi);

            return new AlgorithmIdentifier
            {
                Algorithm = oi.OID,
                Parameters = seq.Elements[1]
            };
        }
    }
}