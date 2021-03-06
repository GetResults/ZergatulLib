﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zergatul.Network.Tls
{
    internal class ServerDHParams
    {
        public byte[] DH_p;
        public byte[] DH_g;
        public byte[] DH_Ys;

        private byte[] _raw;

        public void Read(BinaryReader reader)
        {
            var raw = new List<byte>();
            reader.StartTracking(raw);

            DH_p = reader.ReadBytes(reader.ReadShort());
            DH_g = reader.ReadBytes(reader.ReadShort());
            DH_Ys = reader.ReadBytes(reader.ReadShort());

            reader.StopTracking();

            this._raw = raw.ToArray();
        }

        public byte[] ToBytes()
        {
            if (_raw == null)
                _raw = GetRaw();
            return _raw;
        }

        private byte[] GetRaw()
        {
            var list = new List<byte>();
            var writer = new BinaryWriter(list);

            writer.WriteShort((ushort)DH_p.Length);
            writer.WriteBytes(DH_p);
            writer.WriteShort((ushort)DH_g.Length);
            writer.WriteBytes(DH_g);
            writer.WriteShort((ushort)DH_Ys.Length);
            writer.WriteBytes(DH_Ys);

            return list.ToArray();
        }
    }
}
