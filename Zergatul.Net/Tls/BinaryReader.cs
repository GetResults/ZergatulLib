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
                throw new InvalidOperationException("Invalid buffer length");

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
        }

        public byte ReadByte()
        {
            FillBuffer(1);
            return _buffer[0];
        }

        public byte[] ReadBytes(int count)
        {
            var result = new byte[count];

            if (_stream != null)
            {
                int totalRead = 0;
                while (true)
                {
                    totalRead += _stream.Read(result, totalRead, count - totalRead);
                    if (totalRead == count)
                        break;
                }
            }

            if (_array != null)
            {
                Array.Copy(_array, Position, result, 0, count);
            }

            Position += count;

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

        public ReadCounter StartCounter(int totalBytes)
        {
            return new ReadCounter(this, totalBytes);
        }
    }
}