﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zergatul.Network.ASN1
{
    public class BitString : ASN1Element
    {
        public int PadBits { get; private set; }
        public byte[] Data { get; private set; }

        protected override void ReadBody(Stream stream)
        {
            if (Length < 1)
                throw new InvalidOperationException();
            int readResult = stream.ReadByte();
            if (readResult == -1)
                throw new EndOfStreamException();
            PadBits = readResult;
            Data = ReadBuffer(stream, checked((int)Length - 1));
        }
    }
}