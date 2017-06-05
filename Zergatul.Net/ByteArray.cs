﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zergatul.Cryptography;

namespace Zergatul.Net
{
    internal class ByteArray
    {
        private List<byte[]> _parts;

        public int Length { get; private set; }

        public byte this[int index]
        {
            get
            {
                int partIndex = 0;
                while (partIndex < _parts.Count && index >= _parts[partIndex].Length)
                {
                    index -= _parts[partIndex].Length;
                    partIndex++;
                }
                if (partIndex >= _parts.Count)
                    throw new IndexOutOfRangeException();
                return _parts[partIndex][index];
            }
        }

        public ByteArray()
        {
            this._parts = new List<byte[]>();
            this.Length = 0;
        }

        public ByteArray(byte value)
            : this(new byte[] { value })
        {
        }

        public ByteArray(ushort value)
            : this(new byte[] { (byte)((value >> 8) & 0xFF), (byte)(value & 0xFF) })
        {
        }

        public ByteArray(ulong value)
            : this(new byte[]
            {
                (byte)((value >> 56) & 0xFF),
                (byte)((value >> 48) & 0xFF),
                (byte)((value >> 40) & 0xFF),
                (byte)((value >> 32) & 0xFF),
                (byte)((value >> 24) & 0xFF),
                (byte)((value >> 16) & 0xFF),
                (byte)((value >> 8) & 0xFF),
                (byte)(value & 0xFF),
            })
        {
        }

        public ByteArray(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException();

            this._parts = new List<byte[]> { data };
            this.Length = data.Length;
        }

        public ByteArray(ISecureRandom random, int length)
        {
            if (random == null)
                throw new ArgumentNullException();

            var data = new byte[length];
            random.GetBytes(data);

            this._parts = new List<byte[]> { data };
            this.Length = length;
        }

        public void AddTo(List<byte> list)
        {
            for (int i = 0; i < _parts.Count; i++)
                list.AddRange(_parts[i]);
        }

        public void ClearMemory()
        {
            for (int i = 0; i < _parts.Count; i++)
                for (int j = 0; j < _parts[i].Length; j++)
                    _parts[i][j] = 0;
        }

        public ByteArray SubArray(int start, int length)
        {
            if (start + length > length)
                throw new ArgumentException();

            var bytes = new byte[length];
            for (int i = 0; i < length; i++)
                bytes[i] = this[start + i];
            return new ByteArray(bytes);
        }

        public ByteArray Truncate(int length)
        {
            if (length == this.Length)
                return this;

            var bytes = new byte[length];
            int position = 0;
            int partIndex = 0;
            while (position < length)
            {
                Array.Copy(_parts[partIndex], 0, bytes, position, System.Math.Min(_parts[partIndex].Length, length - position));
                position += _parts[partIndex].Length;
                partIndex++;
            }
            return new ByteArray(bytes);
        }

        public byte[] ToArray()
        {
            var bytes = new byte[Length];
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = this[i];
            return bytes;
        }

        public static ByteArray operator+(ByteArray left, ByteArray right)
        {
            var result = new ByteArray();
            result._parts.AddRange(left._parts);
            result._parts.AddRange(right._parts);
            result.Length = left.Length + right.Length;
            return result;
        }

        public static ByteArray operator +(byte[] left, ByteArray right)
        {
            var result = new ByteArray();
            result._parts.Add(left);
            result._parts.AddRange(right._parts);
            result.Length = left.Length + right.Length;
            return result;
        }

        public static ByteArray operator +(ByteArray left, byte[] right)
        {
            var result = new ByteArray();
            result._parts.AddRange(left._parts);
            result._parts.Add(right);
            result.Length = left.Length + right.Length;
            return result;
        }

        public static ByteArray operator^(ByteArray left, byte right)
        {
            var bytes = left.ToArray();
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = (byte)(left[i] ^ right);
            return new ByteArray(bytes);
        }
    }
}