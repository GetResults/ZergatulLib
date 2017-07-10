﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zergatul.Network.Tls.CipherSuites;
using Zergatul.Network.Tls.Extensions;

namespace Zergatul.Network.Tls
{
    internal class ServerHello : HandshakeBody
    {
        public ProtocolVersion ServerVersion;
        public byte[] Random;
        public byte[] SessionID = new byte[0];
        public CipherSuiteType CipherSuite;
        public List<TlsExtension> Extensions = new List<TlsExtension>();

        private ushort ExtensionsLength => (ushort)Extensions.Sum(e => 4 + e.Length);

        public ServerHello() : base(HandshakeType.ServerHello) { }

        public override void Read(BinaryReader reader)
        {
            ServerVersion = (ProtocolVersion)reader.ReadShort();
            Random = reader.ReadBytes(32);
            SessionID = reader.ReadBytes(reader.ReadByte());
            CipherSuite = (CipherSuiteType)reader.ReadShort();
            var compressionMethod = reader.ReadByte();

            var counter = reader.StartCounter(reader.ReadShort());
            while (counter.CanRead)
            {
                Extensions.Add(new TlsExtension
                {
                    Type = (ExtensionType)reader.ReadShort(),
                    Data = reader.ReadBytes(reader.ReadShort())
                });
            }
        }

        public override void WriteTo(BinaryWriter writer)
        {
            writer.WriteShort((ushort)ServerVersion);
            writer.WriteBytes(Random);
            writer.WriteByte((byte)SessionID.Length);
            // TODO: assume no session
            writer.WriteShort((ushort)CipherSuite);
            writer.WriteByte(0); // compression method

            writer.WriteShort(ExtensionsLength);
            foreach (var ext in Extensions)
            {
                writer.WriteShort((ushort)ext.Type);
                writer.WriteShort(ext.Length);
                writer.WriteBytes(ext.Data);
            }
        }
    }
}