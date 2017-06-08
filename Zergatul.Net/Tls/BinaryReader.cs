﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stream = System.IO.Stream;

namespace Zergatul.Net.Tls
{
    internal class BinaryReader
    {
        private Stream _stream;
        private byte[] _array;
        private byte[] _buffer = new byte[4];

        internal int Position { get; private set; }
        private int? _limit;

        private List<byte> _tracking;

        public BinaryReader(Stream stream)
        {
            this._stream = stream;
            this._array = null;

            Position = 0;
        }

        public BinaryReader(byte[] array)
        {
            this._stream = null;
            this._array = array;

            Position = 0;
        }

        private void FillBuffer(int count)
        {
            if (count > _buffer.Length)
                _buffer = new byte[count];

            if (_stream != null)
            {
                int totalRead = 0;
                while (true)
                {
                    totalRead += _stream.Read(_buffer, totalRead, count - totalRead);
                    if (totalRead == count)
                        break;
                }
            }

            if (_array != null)
            {
                Array.Copy(_array, Position, _buffer, 0, count);
            }

            Position += count;

            if (_tracking != null)
                for (int i = 0; i < count; i++)
                    _tracking.Add(_buffer[i]);
        }

        public byte ReadByte()
        {
            FillBuffer(1);
            return _buffer[0];
        }

        public byte[] ReadBytes(int count)
        {
            FillBuffer(count);

            var result = new byte[count];
            Array.Copy(_buffer, result, count);

            return result;
        }

        public ushort ReadShort()
        {
            FillBuffer(2);
            return (ushort)(_buffer[0] << 8 | _buffer[1]);
        }

        public int ReadUInt24()
        {
            FillBuffer(3);
            return (_buffer[0] << 16) | (_buffer[1] << 8) | _buffer[2];
        }

        public uint ReadUInt32()
        {
            FillBuffer(4);
            return (uint)((_buffer[0] << 24) | (_buffer[1] << 16) | (_buffer[2] << 8) | _buffer[3]);
        }

        public byte[] ReadToEnd()
        {
            if (_limit == null || Position > _limit.Value)
                throw new InvalidOperationException();
            return ReadBytes(_limit.Value - Position);
        }

        public ReadCounter StartCounter(int totalBytes)
        {
            return new ReadCounter(this, totalBytes);
        }

        public void StartTracking(List<byte> data)
        {
            this._tracking = data;
        }

        public void StopTracking()
        {
            this._tracking = null;
        }

        public IDisposable SetReadLimit(int totalBytes)
        {
            _limit = Position + totalBytes;
            return new ReadLimit(this);
        }

        private class ReadLimit : IDisposable
        {
            private BinaryReader _br;

            public ReadLimit(BinaryReader br)
            {
                this._br = br;
            }

            public void Dispose()
            {
                if (_br.Position != _br._limit)
                    throw new InvalidOperationException();
                _br._limit = null;
            }
        }
    }
}