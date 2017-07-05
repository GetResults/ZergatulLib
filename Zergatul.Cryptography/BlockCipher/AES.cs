﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Zergatul.Cryptography.BlockCipher
{
    public abstract class AES : AbstractBlockCipher
    {
        public override int BlockSize => Nb * 4;
        public override int KeySize => Nk * 4;

        private static readonly byte[] SBox = new byte[]
        {
            0x63, 0x7C, 0x77, 0x7B, 0xF2, 0x6B, 0x6F, 0xC5, 0x30, 0x01, 0x67, 0x2B, 0xFE, 0xD7, 0xAB, 0x76,
            0xCA, 0x82, 0xC9, 0x7D, 0xFA, 0x59, 0x47, 0xF0, 0xAD, 0xD4, 0xA2, 0xAF, 0x9C, 0xA4, 0x72, 0xC0,
            0xB7, 0xFD, 0x93, 0x26, 0x36, 0x3F, 0xF7, 0xCC, 0x34, 0xA5, 0xE5, 0xF1, 0x71, 0xD8, 0x31, 0x15,
            0x04, 0xC7, 0x23, 0xC3, 0x18, 0x96, 0x05, 0x9A, 0x07, 0x12, 0x80, 0xE2, 0xEB, 0x27, 0xB2, 0x75,
            0x09, 0x83, 0x2C, 0x1A, 0x1B, 0x6E, 0x5A, 0xA0, 0x52, 0x3B, 0xD6, 0xB3, 0x29, 0xE3, 0x2F, 0x84,
            0x53, 0xD1, 0x00, 0xED, 0x20, 0xFC, 0xB1, 0x5B, 0x6A, 0xCB, 0xBE, 0x39, 0x4A, 0x4C, 0x58, 0xCF,
            0xD0, 0xEF, 0xAA, 0xFB, 0x43, 0x4D, 0x33, 0x85, 0x45, 0xF9, 0x02, 0x7F, 0x50, 0x3C, 0x9F, 0xA8,
            0x51, 0xA3, 0x40, 0x8F, 0x92, 0x9D, 0x38, 0xF5, 0xBC, 0xB6, 0xDA, 0x21, 0x10, 0xFF, 0xF3, 0xD2,
            0xCD, 0x0C, 0x13, 0xEC, 0x5F, 0x97, 0x44, 0x17, 0xC4, 0xA7, 0x7E, 0x3D, 0x64, 0x5D, 0x19, 0x73,
            0x60, 0x81, 0x4F, 0xDC, 0x22, 0x2A, 0x90, 0x88, 0x46, 0xEE, 0xB8, 0x14, 0xDE, 0x5E, 0x0B, 0xDB,
            0xE0, 0x32, 0x3A, 0x0A, 0x49, 0x06, 0x24, 0x5C, 0xC2, 0xD3, 0xAC, 0x62, 0x91, 0x95, 0xE4, 0x79,
            0xE7, 0xC8, 0x37, 0x6D, 0x8D, 0xD5, 0x4E, 0xA9, 0x6C, 0x56, 0xF4, 0xEA, 0x65, 0x7A, 0xAE, 0x08,
            0xBA, 0x78, 0x25, 0x2E, 0x1C, 0xA6, 0xB4, 0xC6, 0xE8, 0xDD, 0x74, 0x1F, 0x4B, 0xBD, 0x8B, 0x8A,
            0x70, 0x3E, 0xB5, 0x66, 0x48, 0x03, 0xF6, 0x0E, 0x61, 0x35, 0x57, 0xB9, 0x86, 0xC1, 0x1D, 0x9E,
            0xE1, 0xF8, 0x98, 0x11, 0x69, 0xD9, 0x8E, 0x94, 0x9B, 0x1E, 0x87, 0xE9, 0xCE, 0x55, 0x28, 0xDF,
            0x8C, 0xA1, 0x89, 0x0D, 0xBF, 0xE6, 0x42, 0x68, 0x41, 0x99, 0x2D, 0x0F, 0xB0, 0x54, 0xBB, 0x16
        };

        private static readonly byte[] Rcon = new byte[]
        {
            0x01, 0x02, 0x04, 0x08, 0x10, 0x20, 0x40, 0x80, 0x1B, 0x36, 0x6C, 0xD8, 0xAB, 0x4D, 0x9A,
            0x2F, 0x5E, 0xBC, 0x63, 0xC6, 0x97, 0x35, 0x6A, 0xD4, 0xB3, 0x7D, 0xFA, 0xEF, 0xC5, 0x91
        };

        private static readonly int[,] MixColumnMatrix = new int[,]
        {
            { 2, 3, 1, 1 },
            { 1, 2, 3, 1 },
            { 1, 1, 2, 3 },
            { 3, 1, 1, 2 }
        };

        int Nb, Nk, Nr;

        public AES(int Nb, int Nk, int Nr)
        {
            this.Nb = Nb;
            this.Nk = Nk;
            this.Nr = Nr;
        }

        private uint[] KeyExpansion(byte[] key)
        {
            uint[] w = new uint[Nb * (Nr + 1)];

            for (int i = 0; i < Nk; i++)
                w[i] = BitHelper.ToUInt32(key, 4 * i, ByteOrder.BigEndian);

            for (int i = Nk; i < w.Length; i++)
            {
                uint temp = w[i - 1];
                if (i % Nk == 0)
                    temp = SubWord(RotWord(temp)) ^ (uint)(Rcon[i / Nk - 1] << 24);
                else if (Nk > 6 && i % Nk == 4)
                    temp = SubWord(temp);
                w[i] = w[i - Nk] ^ temp;
            }

            return w;
        }

        private static uint SubWord(uint value)
        {
            return (uint)(
                (SBox[(value >> 00) & 0xFF] << 00) |
                (SBox[(value >> 08) & 0xFF] << 08) |
                (SBox[(value >> 16) & 0xFF] << 16) |
                (SBox[(value >> 24) & 0xFF] << 24));
        }

        private static uint RotWord(uint value)
        {
            return BitHelper.RotateLeft(value, 8);
        }

        private static void AddRoundKey(State state, uint[] w, int index)
        {
            for (int i = 0; i < 4; i++)
                state[i] ^= w[index + i];
        }

        private static void SubBytes(State state)
        {
            for (int i = 0; i < 4; i++)
                state[i] = SubWord(state[i]);
        }

        private static void ShiftRows(State state)
        {
            for (int i = 1; i < 4; i++)
            {
                byte[] row = new byte[] { state[i, 0], state[i, 1], state[i, 2], state[i, 3] };
                for (int j = 0; j < 4; j++)
                    state[i, j] = row[(i + j) % 4];
            }
        }

        private static byte GaloisMult(byte a, byte b)
        {
            byte result = 0;
            for (int i = 0; i < 8; i++)
            {
                if ((b & 1) != 0)
                    result ^= a;
                bool highBitSet = (a * 0x80) != 0;
                a <<= 1;
                if (highBitSet)
                    a ^= 0x1B;
                b >>= 1;
            }
            return result;
        }

        private static void MixColumns(State state)
        {
            byte[,] newState = new byte[4, 4];

            for (int i = 0; i < 4; i++)
            {
                newState[0, i] = (byte)(GaloisMult(2, state[0, i]) ^ GaloisMult(3, state[1, i]) ^ state[2, i] ^ state[3, i]);
                newState[1, i] = (byte)(state[0, i] ^ GaloisMult(2, state[1, i]) ^ GaloisMult(3, state[2, i]) ^ state[3, i]);
                newState[2, i] = (byte)(state[0, i] ^ state[1, i] ^ GaloisMult(2, state[2, i]) ^ GaloisMult(3, state[3, i]));
                newState[3, i] = (byte)(GaloisMult(3, state[0, i]) ^ state[1, i] ^ state[2, i] ^ GaloisMult(2, state[3, i]));
            }

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    state[i, j] = newState[i, j];
        }

        public override Encryptor CreateEncryptor(byte[] key, BlockCipherMode mode)
        {
            uint[] w = KeyExpansion(key);

            var encryptor = ResolveEncryptor(mode);
            encryptor.Cipher = this;
            encryptor.ProcessBlock = (block) =>
            {
                var state = new State();

                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 4; j++)
                        state[i, j] = block[j * 4 + i];

                AddRoundKey(state, w, 0);

                for (int round = 1; round < Nr; round++)
                {
                    SubBytes(state);
                    ShiftRows(state);
                    MixColumns(state);
                    AddRoundKey(state, w, round * Nb);
                }

                SubBytes(state);
                ShiftRows(state);
                AddRoundKey(state, w, Nr * Nb);

                return null;
            };

            return encryptor;
        }

        public override Decryptor CreateDecryptor(byte[] key, BlockCipherMode mode)
        {
            throw new NotImplementedException();
        }

        private class State
        {
            private byte[,] s = new byte[4, 4];

            public uint this[int i]
            {
                get
                {
                    return BitHelper.ToUInt32(new byte[] { s[0, i], s[1, i], s[2, i], s[3, i] }, ByteOrder.BigEndian);
                }
                set
                {
                    var bytes = BitHelper.GetBytes(value, ByteOrder.BigEndian);
                    s[0, i] = bytes[0];
                    s[1, i] = bytes[1];
                    s[2, i] = bytes[2];
                    s[3, i] = bytes[3];
                }
            }

            public byte this[int i, int j]
            {
                get
                {
                    return s[i, j];
                }
                set
                {
                    s[i, j] = value;
                }
            }

            public override string ToString()
            {
                var sb = new StringBuilder();

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                        sb.Append(s[i, j].ToString("x2") + "|");
                    sb.Remove(sb.Length - 1, 1);
                    sb.AppendLine();
                }

                return sb.ToString();
            }
        }
    }
}
