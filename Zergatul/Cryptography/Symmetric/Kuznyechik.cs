﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zergatul.Math;

namespace Zergatul.Cryptography.Symmetric
{
    /// <summary>
    /// GOST R 34.12-2015
    /// </summary>
    public class Kuznyechik : AbstractBlockCipher
    {
        public override int BlockSize => 16;
        public override int KeySize => 32;

        private static readonly byte[] Pi = new byte[]
        {
            0xFC, 0xEE, 0xDD, 0x11, 0xCF, 0x6E, 0x31, 0x16, 0xFB, 0xC4, 0xFA, 0xDA, 0x23, 0xC5, 0x04, 0x4D,
            0xE9, 0x77, 0xF0, 0xDB, 0x93, 0x2E, 0x99, 0xBA, 0x17, 0x36, 0xF1, 0xBB, 0x14, 0xCD, 0x5F, 0xC1,
            0xF9, 0x18, 0x65, 0x5A, 0xE2, 0x5C, 0xEF, 0x21, 0x81, 0x1C, 0x3C, 0x42, 0x8B, 0x01, 0x8E, 0x4F,
            0x05, 0x84, 0x02, 0xAE, 0xE3, 0x6A, 0x8F, 0xA0, 0x06, 0x0B, 0xED, 0x98, 0x7F, 0xD4, 0xD3, 0x1F,
            0xEB, 0x34, 0x2C, 0x51, 0xEA, 0xC8, 0x48, 0xAB, 0xF2, 0x2A, 0x68, 0xA2, 0xFD, 0x3A, 0xCE, 0xCC,
            0xB5, 0x70, 0x0E, 0x56, 0x08, 0x0C, 0x76, 0x12, 0xBF, 0x72, 0x13, 0x47, 0x9C, 0xB7, 0x5D, 0x87,
            0x15, 0xA1, 0x96, 0x29, 0x10, 0x7B, 0x9A, 0xC7, 0xF3, 0x91, 0x78, 0x6F, 0x9D, 0x9E, 0xB2, 0xB1,
            0x32, 0x75, 0x19, 0x3D, 0xFF, 0x35, 0x8A, 0x7E, 0x6D, 0x54, 0xC6, 0x80, 0xC3, 0xBD, 0x0D, 0x57,
            0xDF, 0xF5, 0x24, 0xA9, 0x3E, 0xA8, 0x43, 0xC9, 0xD7, 0x79, 0xD6, 0xF6, 0x7C, 0x22, 0xB9, 0x03,
            0xE0, 0x0F, 0xEC, 0xDE, 0x7A, 0x94, 0xB0, 0xBC, 0xDC, 0xE8, 0x28, 0x50, 0x4E, 0x33, 0x0A, 0x4A,
            0xA7, 0x97, 0x60, 0x73, 0x1E, 0x00, 0x62, 0x44, 0x1A, 0xB8, 0x38, 0x82, 0x64, 0x9F, 0x26, 0x41,
            0xAD, 0x45, 0x46, 0x92, 0x27, 0x5E, 0x55, 0x2F, 0x8C, 0xA3, 0xA5, 0x7D, 0x69, 0xD5, 0x95, 0x3B,
            0x07, 0x58, 0xB3, 0x40, 0x86, 0xAC, 0x1D, 0xF7, 0x30, 0x37, 0x6B, 0xE4, 0x88, 0xD9, 0xE7, 0x89,
            0xE1, 0x1B, 0x83, 0x49, 0x4C, 0x3F, 0xF8, 0xFE, 0x8D, 0x53, 0xAA, 0x90, 0xCA, 0xD8, 0x85, 0x61,
            0x20, 0x71, 0x67, 0xA4, 0x2D, 0x2B, 0x09, 0x5B, 0xCB, 0x9B, 0x25, 0xD0, 0xBE, 0xE5, 0x6C, 0x52,
            0x59, 0xA6, 0x74, 0xD2, 0xE6, 0xF4, 0xB4, 0xC0, 0xD1, 0x66, 0xAF, 0xC2, 0x39, 0x4B, 0x63, 0xB6,
        };

        private static readonly byte[] PiInv = new byte[]
        {
            0xA5, 0x2D, 0x32, 0x8F, 0x0E, 0x30, 0x38, 0xC0, 0x54, 0xE6, 0x9E, 0x39, 0x55, 0x7E, 0x52, 0x91,
            0x64, 0x03, 0x57, 0x5A, 0x1C, 0x60, 0x07, 0x18, 0x21, 0x72, 0xA8, 0xD1, 0x29, 0xC6, 0xA4, 0x3F,
            0xE0, 0x27, 0x8D, 0x0C, 0x82, 0xEA, 0xAE, 0xB4, 0x9A, 0x63, 0x49, 0xE5, 0x42, 0xE4, 0x15, 0xB7,
            0xC8, 0x06, 0x70, 0x9D, 0x41, 0x75, 0x19, 0xC9, 0xAA, 0xFC, 0x4D, 0xBF, 0x2A, 0x73, 0x84, 0xD5,
            0xC3, 0xAF, 0x2B, 0x86, 0xA7, 0xB1, 0xB2, 0x5B, 0x46, 0xD3, 0x9F, 0xFD, 0xD4, 0x0F, 0x9C, 0x2F,
            0x9B, 0x43, 0xEF, 0xD9, 0x79, 0xB6, 0x53, 0x7F, 0xC1, 0xF0, 0x23, 0xE7, 0x25, 0x5E, 0xB5, 0x1E,
            0xA2, 0xDF, 0xA6, 0xFE, 0xAC, 0x22, 0xF9, 0xE2, 0x4A, 0xBC, 0x35, 0xCA, 0xEE, 0x78, 0x05, 0x6B,
            0x51, 0xE1, 0x59, 0xA3, 0xF2, 0x71, 0x56, 0x11, 0x6A, 0x89, 0x94, 0x65, 0x8C, 0xBB, 0x77, 0x3C,
            0x7B, 0x28, 0xAB, 0xD2, 0x31, 0xDE, 0xC4, 0x5F, 0xCC, 0xCF, 0x76, 0x2C, 0xB8, 0xD8, 0x2E, 0x36,
            0xDB, 0x69, 0xB3, 0x14, 0x95, 0xBE, 0x62, 0xA1, 0x3B, 0x16, 0x66, 0xE9, 0x5C, 0x6C, 0x6D, 0xAD,
            0x37, 0x61, 0x4B, 0xB9, 0xE3, 0xBA, 0xF1, 0xA0, 0x85, 0x83, 0xDA, 0x47, 0xC5, 0xB0, 0x33, 0xFA,
            0x96, 0x6F, 0x6E, 0xC2, 0xF6, 0x50, 0xFF, 0x5D, 0xA9, 0x8E, 0x17, 0x1B, 0x97, 0x7D, 0xEC, 0x58,
            0xF7, 0x1F, 0xFB, 0x7C, 0x09, 0x0D, 0x7A, 0x67, 0x45, 0x87, 0xDC, 0xE8, 0x4F, 0x1D, 0x4E, 0x04,
            0xEB, 0xF8, 0xF3, 0x3E, 0x3D, 0xBD, 0x8A, 0x88, 0xDD, 0xCD, 0x0B, 0x13, 0x98, 0x02, 0x93, 0x80,
            0x90, 0xD0, 0x24, 0x34, 0xCB, 0xED, 0xF4, 0xCE, 0x99, 0x10, 0x44, 0x40, 0x92, 0x3A, 0x01, 0x26,
            0x12, 0x1A, 0x48, 0x68, 0xF5, 0x81, 0x8B, 0xC7, 0xD6, 0x20, 0x0A, 0x08, 0x00, 0x4C, 0xD7, 0x74,
        };

        private static readonly byte[][] C = new byte[][]
        {
            new byte[] { 0x6E, 0xA2, 0x76, 0x72, 0x6C, 0x48, 0x7A, 0xB8, 0x5D, 0x27, 0xBD, 0x10, 0xDD, 0x84, 0x94, 0x01 },
            new byte[] { 0xDC, 0x87, 0xEC, 0xE4, 0xD8, 0x90, 0xF4, 0xB3, 0xBA, 0x4E, 0xB9, 0x20, 0x79, 0xCB, 0xEB, 0x02 },
            new byte[] { 0xB2, 0x25, 0x9A, 0x96, 0xB4, 0xD8, 0x8E, 0x0B, 0xE7, 0x69, 0x04, 0x30, 0xA4, 0x4F, 0x7F, 0x03 },
            new byte[] { 0x7B, 0xCD, 0x1B, 0x0B, 0x73, 0xE3, 0x2B, 0xA5, 0xB7, 0x9C, 0xB1, 0x40, 0xF2, 0x55, 0x15, 0x04 },
            new byte[] { 0x15, 0x6F, 0x6D, 0x79, 0x1F, 0xAB, 0x51, 0x1D, 0xEA, 0xBB, 0x0C, 0x50, 0x2F, 0xD1, 0x81, 0x05 },
            new byte[] { 0xA7, 0x4A, 0xF7, 0xEF, 0xAB, 0x73, 0xDF, 0x16, 0x0D, 0xD2, 0x08, 0x60, 0x8B, 0x9E, 0xFE, 0x06 },
            new byte[] { 0xC9, 0xE8, 0x81, 0x9D, 0xC7, 0x3B, 0xA5, 0xAE, 0x50, 0xF5, 0xB5, 0x70, 0x56, 0x1A, 0x6A, 0x07 },
            new byte[] { 0xF6, 0x59, 0x36, 0x16, 0xE6, 0x05, 0x56, 0x89, 0xAD, 0xFB, 0xA1, 0x80, 0x27, 0xAA, 0x2A, 0x08 },
            new byte[] { 0x98, 0xFB, 0x40, 0x64, 0x8A, 0x4D, 0x2C, 0x31, 0xF0, 0xDC, 0x1C, 0x90, 0xFA, 0x2E, 0xBE, 0x09 },
            new byte[] { 0x2A, 0xDE, 0xDA, 0xF2, 0x3E, 0x95, 0xA2, 0x3A, 0x17, 0xB5, 0x18, 0xA0, 0x5E, 0x61, 0xC1, 0x0A },
            new byte[] { 0x44, 0x7C, 0xAC, 0x80, 0x52, 0xDD, 0xD8, 0x82, 0x4A, 0x92, 0xA5, 0xB0, 0x83, 0xE5, 0x55, 0x0B },
            new byte[] { 0x8D, 0x94, 0x2D, 0x1D, 0x95, 0xE6, 0x7D, 0x2C, 0x1A, 0x67, 0x10, 0xC0, 0xD5, 0xFF, 0x3F, 0x0C },
            new byte[] { 0xE3, 0x36, 0x5B, 0x6F, 0xF9, 0xAE, 0x07, 0x94, 0x47, 0x40, 0xAD, 0xD0, 0x08, 0x7B, 0xAB, 0x0D },
            new byte[] { 0x51, 0x13, 0xC1, 0xF9, 0x4D, 0x76, 0x89, 0x9F, 0xA0, 0x29, 0xA9, 0xE0, 0xAC, 0x34, 0xD4, 0x0E },
            new byte[] { 0x3F, 0xB1, 0xB7, 0x8B, 0x21, 0x3E, 0xF3, 0x27, 0xFD, 0x0E, 0x14, 0xF0, 0x71, 0xB0, 0x40, 0x0F },
            new byte[] { 0x2F, 0xB2, 0x6C, 0x2C, 0x0F, 0x0A, 0xAC, 0xD1, 0x99, 0x35, 0x81, 0xC3, 0x4E, 0x97, 0x54, 0x10 },
            new byte[] { 0x41, 0x10, 0x1A, 0x5E, 0x63, 0x42, 0xD6, 0x69, 0xC4, 0x12, 0x3C, 0xD3, 0x93, 0x13, 0xC0, 0x11 },
            new byte[] { 0xF3, 0x35, 0x80, 0xC8, 0xD7, 0x9A, 0x58, 0x62, 0x23, 0x7B, 0x38, 0xE3, 0x37, 0x5C, 0xBF, 0x12 },
            new byte[] { 0x9D, 0x97, 0xF6, 0xBA, 0xBB, 0xD2, 0x22, 0xDA, 0x7E, 0x5C, 0x85, 0xF3, 0xEA, 0xD8, 0x2B, 0x13 },
            new byte[] { 0x54, 0x7F, 0x77, 0x27, 0x7C, 0xE9, 0x87, 0x74, 0x2E, 0xA9, 0x30, 0x83, 0xBC, 0xC2, 0x41, 0x14 },
            new byte[] { 0x3A, 0xDD, 0x01, 0x55, 0x10, 0xA1, 0xFD, 0xCC, 0x73, 0x8E, 0x8D, 0x93, 0x61, 0x46, 0xD5, 0x15 },
            new byte[] { 0x88, 0xF8, 0x9B, 0xC3, 0xA4, 0x79, 0x73, 0xC7, 0x94, 0xE7, 0x89, 0xA3, 0xC5, 0x09, 0xAA, 0x16 },
            new byte[] { 0xE6, 0x5A, 0xED, 0xB1, 0xC8, 0x31, 0x09, 0x7F, 0xC9, 0xC0, 0x34, 0xB3, 0x18, 0x8D, 0x3E, 0x17 },
            new byte[] { 0xD9, 0xEB, 0x5A, 0x3A, 0xE9, 0x0F, 0xFA, 0x58, 0x34, 0xCE, 0x20, 0x43, 0x69, 0x3D, 0x7E, 0x18 },
            new byte[] { 0xB7, 0x49, 0x2C, 0x48, 0x85, 0x47, 0x80, 0xE0, 0x69, 0xE9, 0x9D, 0x53, 0xB4, 0xB9, 0xEA, 0x19 },
            new byte[] { 0x05, 0x6C, 0xB6, 0xDE, 0x31, 0x9F, 0x0E, 0xEB, 0x8E, 0x80, 0x99, 0x63, 0x10, 0xF6, 0x95, 0x1A },
            new byte[] { 0x6B, 0xCE, 0xC0, 0xAC, 0x5D, 0xD7, 0x74, 0x53, 0xD3, 0xA7, 0x24, 0x73, 0xCD, 0x72, 0x01, 0x1B },
            new byte[] { 0xA2, 0x26, 0x41, 0x31, 0x9A, 0xEC, 0xD1, 0xFD, 0x83, 0x52, 0x91, 0x03, 0x9B, 0x68, 0x6B, 0x1C },
            new byte[] { 0xCC, 0x84, 0x37, 0x43, 0xF6, 0xA4, 0xAB, 0x45, 0xDE, 0x75, 0x2C, 0x13, 0x46, 0xEC, 0xFF, 0x1D },
            new byte[] { 0x7E, 0xA1, 0xAD, 0xD5, 0x42, 0x7C, 0x25, 0x4E, 0x39, 0x1C, 0x28, 0x23, 0xE2, 0xA3, 0x80, 0x1E },
            new byte[] { 0x10, 0x03, 0xDB, 0xA7, 0x2E, 0x34, 0x5F, 0xF6, 0x64, 0x3B, 0x95, 0x33, 0x3F, 0x27, 0x14, 0x1F },
            new byte[] { 0x5E, 0xA7, 0xD8, 0x58, 0x1E, 0x14, 0x9B, 0x61, 0xF1, 0x6A, 0xC1, 0x45, 0x9C, 0xED, 0xA8, 0x20 },
        };

        private static byte l(byte[] a)
        {
            BinaryPolynomial bp = BinaryPolynomial.FromPowers(8, 7, 6, 1, 0);

            var order = ByteOrder.LittleEndian;
            var coefs = new BinaryPolynomial[]
            {
                new BinaryPolynomial(new byte[] { 148 }, order),
                new BinaryPolynomial(new byte[] {  32 }, order),
                new BinaryPolynomial(new byte[] { 133 }, order),
                new BinaryPolynomial(new byte[] {  16 }, order),
                new BinaryPolynomial(new byte[] { 194 }, order),
                new BinaryPolynomial(new byte[] { 192 }, order),
                new BinaryPolynomial(new byte[] {   1 }, order),
                new BinaryPolynomial(new byte[] { 251 }, order),
                new BinaryPolynomial(new byte[] {   1 }, order),
                new BinaryPolynomial(new byte[] { 192 }, order),
                new BinaryPolynomial(new byte[] { 194 }, order),
                new BinaryPolynomial(new byte[] {  16 }, order),
                new BinaryPolynomial(new byte[] { 133 }, order),
                new BinaryPolynomial(new byte[] {  32 }, order),
                new BinaryPolynomial(new byte[] { 148 }, order),
                new BinaryPolynomial(new byte[] {   1 }, order),
            };

            var result = BinaryPolynomial.Zero;
            for (int i = 0; i < 16; i++)
                result += BinaryPolynomial.ModularMultiplication(coefs[i], new BinaryPolynomial(new byte[] { a[i] }, order), bp);

            return result.ToBytes(order, 1)[0];
        }

        private static void F(byte[] C, byte[] a0, byte[] a1, out byte[] r0, out byte[] r1)
        {
            byte[] buf = new byte[16];

            // X[C](a1)
            for (int i = 0; i < 16; i++)
                buf[i] = (byte)(C[i] ^ a1[i]);

            // SX[C](a1)
            for (int i = 0; i < 16; i++)
                buf[i] = Pi[buf[i]];

            // LSX[C](a1)
            for (int round = 0; round < 16; round++)
            {
                byte lres = l(buf);
                for (int i = 15; i >= 1; i--)
                    buf[i] = buf[i - 1];
                buf[0] = lres;
            }

            // LSX[C](a1) xor a0
            for (int i = 0; i < 16; i++)
                buf[i] ^= a0[i];

            r0 = buf;
            r1 = a1;
        }

        private static void KeyExpansion(byte[] key, out byte[][] K)
        {
            K = new byte[10][];
            K[0] = new byte[16];
            K[1] = new byte[16];

            Array.Copy(key, 0, K[0], 0, 16);
            Array.Copy(key, 16, K[1], 0, 16);

            for (int i = 1; i <= 4; i++)
            {
                byte[] r0, r1;
                byte[] a0 = K[2 * i - 2], a1 = K[2 * i - 1];
                for (int j = 0; j < 8; j++)
                {
                    F(C[8 * (i - 1) + j], a1, a0, out r0, out r1);
                    a0 = r0;
                    a1 = r1;
                }

                K[i * 2] = new byte[16];
                K[i * 2 + 1] = new byte[16];
                Array.Copy(a0, 0, K[i * 2], 0, 16);
                Array.Copy(a1, 0, K[i * 2 + 1], 0, 16);
            }
        }

        public override Func<byte[], byte[]> CreateEncryptor(byte[] key)
        {
            byte[][] K;
            KeyExpansion(key, out K);

            return (block) =>
            {
                byte[] result = new byte[16];
                Array.Copy(block, 0, result, 0, 16);

                for (int round = 0; round < 10; round++)
                {
                    // X[Ki](a)
                    for (int i = 0; i < 16; i++)
                        result[i] ^= K[round][i];

                    if (round == 9)
                        break;

                    // SX[Ki](a)
                    for (int i = 0; i < 16; i++)
                        result[i] = Pi[result[i]];

                    // LSX[Ki](a)
                    for (int c = 0; c < 16; c++)
                    {
                        byte lres = l(result);
                        for (int i = 15; i >= 1; i--)
                            result[i] = result[i - 1];
                        result[0] = lres;
                    }
                }

                return result;
            };
        }

        public override Func<byte[], byte[]> CreateDecryptor(byte[] key)
        {
            byte[][] K;
            KeyExpansion(key, out K);

            return (block) =>
            {
                byte[] result = new byte[16];
                Array.Copy(block, 0, result, 0, 16);

                for (int round = 9; round >= 0; round--)
                {
                    // X[Ki](a)
                    for (int i = 0; i < 16; i++)
                        result[i] ^= K[round][i];

                    if (round == 0)
                        break;

                    // L(-1)SX[Ki](a)
                    for (int c = 0; c < 16; c++)
                    {
                        byte buf = result[0];
                        for (int i = 0; i < 15; i++)
                            result[i] = result[i + 1];
                        result[15] = buf;
                        result[15] = l(result);
                    }

                    // S(-1)X[Ki](a)
                    for (int i = 0; i < 16; i++)
                        result[i] = PiInv[result[i]];
                }

                return result;
            };
        }
    }
}