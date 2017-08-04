﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zergatul.Network.Tls
{
    internal class ClientKeyExchange : HandshakeBody
    {
        public byte[] DH_Yc;
        public byte[] ECDH_Yc;

        private AbstractCipherSuite _cipher;

        public ClientKeyExchange(AbstractCipherSuite cipher)
            : base(HandshakeType.ClientKeyExchange)
        {
            this._cipher = cipher;
        }

        public override void Read(BinaryReader reader)
        {
            _cipher.ReadClientKeyExchange(this, reader);
        }

        public override void WriteTo(BinaryWriter writer)
        {
            _cipher.WriteClientKeyExchange(this, writer);
        }
    }
}
