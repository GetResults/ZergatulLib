﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using Zergatul.Cryptography.BlockCipher.CipherMode;
using Zergatul.Cryptography.BlockCipher;

namespace Zergatul.Cryptography.Tests.BlockCipher
{
    [TestClass]
    public class Poly1305FunctionTests
    {
        [TestMethod]
        public void Vector1()
        {
            byte[] key = new byte[]
            {
                0x85, 0xd6, 0xbe, 0x78, 0x57, 0x55, 0x6d, 0x33, 0x7f, 0x44, 0x52, 0xfe, 0x42, 0xd5, 0x06, 0xa8,
                0x01, 0x03, 0x80, 0x8a, 0xfb, 0x0d, 0xb2, 0xfd, 0x4a, 0xbf, 0xf6, 0xaf, 0x41, 0x49, 0xf5, 0x1b
            };
            byte[] msg = Encoding.ASCII.GetBytes("Cryptographic Forum Research Group");
            byte[] tag = new byte[]
            {
                0xa8, 0x06, 0x1d, 0xc1, 0x30, 0x51, 0x36, 0xc6, 0xc2, 0x2b, 0x8b, 0xaf, 0x0c, 0x01, 0x27, 0xa9
            };

            TestTag(key, msg, tag);
        }

        [TestMethod]
        public void TestVector1()
        {
            TestTag(new byte[32], new byte[48], new byte[16]);
        }

        [TestMethod]
        public void TestVector2()
        {
            byte[] key = new byte[]
            {
               0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
               0x36, 0xe5, 0xf6, 0xb5, 0xc5, 0xe0, 0x60, 0x70, 0xf0, 0xef, 0xca, 0x96, 0x22, 0x7a, 0x86, 0x3e
            };
            byte[] msg = new byte[]
            {
                0x41, 0x6e, 0x79, 0x20, 0x73, 0x75, 0x62, 0x6d, 0x69, 0x73, 0x73, 0x69, 0x6f, 0x6e, 0x20, 0x74,
                0x6f, 0x20, 0x74, 0x68, 0x65, 0x20, 0x49, 0x45, 0x54, 0x46, 0x20, 0x69, 0x6e, 0x74, 0x65, 0x6e,
                0x64, 0x65, 0x64, 0x20, 0x62, 0x79, 0x20, 0x74, 0x68, 0x65, 0x20, 0x43, 0x6f, 0x6e, 0x74, 0x72,
                0x69, 0x62, 0x75, 0x74, 0x6f, 0x72, 0x20, 0x66, 0x6f, 0x72, 0x20, 0x70, 0x75, 0x62, 0x6c, 0x69,
                0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x20, 0x61, 0x73, 0x20, 0x61, 0x6c, 0x6c, 0x20, 0x6f, 0x72,
                0x20, 0x70, 0x61, 0x72, 0x74, 0x20, 0x6f, 0x66, 0x20, 0x61, 0x6e, 0x20, 0x49, 0x45, 0x54, 0x46,
                0x20, 0x49, 0x6e, 0x74, 0x65, 0x72, 0x6e, 0x65, 0x74, 0x2d, 0x44, 0x72, 0x61, 0x66, 0x74, 0x20,
                0x6f, 0x72, 0x20, 0x52, 0x46, 0x43, 0x20, 0x61, 0x6e, 0x64, 0x20, 0x61, 0x6e, 0x79, 0x20, 0x73,
                0x74, 0x61, 0x74, 0x65, 0x6d, 0x65, 0x6e, 0x74, 0x20, 0x6d, 0x61, 0x64, 0x65, 0x20, 0x77, 0x69,
                0x74, 0x68, 0x69, 0x6e, 0x20, 0x74, 0x68, 0x65, 0x20, 0x63, 0x6f, 0x6e, 0x74, 0x65, 0x78, 0x74,
                0x20, 0x6f, 0x66, 0x20, 0x61, 0x6e, 0x20, 0x49, 0x45, 0x54, 0x46, 0x20, 0x61, 0x63, 0x74, 0x69,
                0x76, 0x69, 0x74, 0x79, 0x20, 0x69, 0x73, 0x20, 0x63, 0x6f, 0x6e, 0x73, 0x69, 0x64, 0x65, 0x72,
                0x65, 0x64, 0x20, 0x61, 0x6e, 0x20, 0x22, 0x49, 0x45, 0x54, 0x46, 0x20, 0x43, 0x6f, 0x6e, 0x74,
                0x72, 0x69, 0x62, 0x75, 0x74, 0x69, 0x6f, 0x6e, 0x22, 0x2e, 0x20, 0x53, 0x75, 0x63, 0x68, 0x20,
                0x73, 0x74, 0x61, 0x74, 0x65, 0x6d, 0x65, 0x6e, 0x74, 0x73, 0x20, 0x69, 0x6e, 0x63, 0x6c, 0x75,
                0x64, 0x65, 0x20, 0x6f, 0x72, 0x61, 0x6c, 0x20, 0x73, 0x74, 0x61, 0x74, 0x65, 0x6d, 0x65, 0x6e,
                0x74, 0x73, 0x20, 0x69, 0x6e, 0x20, 0x49, 0x45, 0x54, 0x46, 0x20, 0x73, 0x65, 0x73, 0x73, 0x69,
                0x6f, 0x6e, 0x73, 0x2c, 0x20, 0x61, 0x73, 0x20, 0x77, 0x65, 0x6c, 0x6c, 0x20, 0x61, 0x73, 0x20,
                0x77, 0x72, 0x69, 0x74, 0x74, 0x65, 0x6e, 0x20, 0x61, 0x6e, 0x64, 0x20, 0x65, 0x6c, 0x65, 0x63,
                0x74, 0x72, 0x6f, 0x6e, 0x69, 0x63, 0x20, 0x63, 0x6f, 0x6d, 0x6d, 0x75, 0x6e, 0x69, 0x63, 0x61,
                0x74, 0x69, 0x6f, 0x6e, 0x73, 0x20, 0x6d, 0x61, 0x64, 0x65, 0x20, 0x61, 0x74, 0x20, 0x61, 0x6e,
                0x79, 0x20, 0x74, 0x69, 0x6d, 0x65, 0x20, 0x6f, 0x72, 0x20, 0x70, 0x6c, 0x61, 0x63, 0x65, 0x2c,
                0x20, 0x77, 0x68, 0x69, 0x63, 0x68, 0x20, 0x61, 0x72, 0x65, 0x20, 0x61, 0x64, 0x64, 0x72, 0x65,
                0x73, 0x73, 0x65, 0x64, 0x20, 0x74, 0x6f
            };
            byte[] tag = new byte[]
            {
                0x36, 0xe5, 0xf6, 0xb5, 0xc5, 0xe0, 0x60, 0x70, 0xf0, 0xef, 0xca, 0x96, 0x22, 0x7a, 0x86, 0x3e
            };

            TestTag(key, msg, tag);
        }

        [TestMethod]
        public void TestVector3()
        {
            byte[] key = new byte[]
            {
                0x36, 0xe5, 0xf6, 0xb5, 0xc5, 0xe0, 0x60, 0x70, 0xf0, 0xef, 0xca, 0x96, 0x22, 0x7a, 0x86, 0x3e,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };
            byte[] msg = new byte[]
            {
                0x41, 0x6e, 0x79, 0x20, 0x73, 0x75, 0x62, 0x6d, 0x69, 0x73, 0x73, 0x69, 0x6f, 0x6e, 0x20, 0x74,
                0x6f, 0x20, 0x74, 0x68, 0x65, 0x20, 0x49, 0x45, 0x54, 0x46, 0x20, 0x69, 0x6e, 0x74, 0x65, 0x6e,
                0x64, 0x65, 0x64, 0x20, 0x62, 0x79, 0x20, 0x74, 0x68, 0x65, 0x20, 0x43, 0x6f, 0x6e, 0x74, 0x72,
                0x69, 0x62, 0x75, 0x74, 0x6f, 0x72, 0x20, 0x66, 0x6f, 0x72, 0x20, 0x70, 0x75, 0x62, 0x6c, 0x69,
                0x63, 0x61, 0x74, 0x69, 0x6f, 0x6e, 0x20, 0x61, 0x73, 0x20, 0x61, 0x6c, 0x6c, 0x20, 0x6f, 0x72,
                0x20, 0x70, 0x61, 0x72, 0x74, 0x20, 0x6f, 0x66, 0x20, 0x61, 0x6e, 0x20, 0x49, 0x45, 0x54, 0x46,
                0x20, 0x49, 0x6e, 0x74, 0x65, 0x72, 0x6e, 0x65, 0x74, 0x2d, 0x44, 0x72, 0x61, 0x66, 0x74, 0x20,
                0x6f, 0x72, 0x20, 0x52, 0x46, 0x43, 0x20, 0x61, 0x6e, 0x64, 0x20, 0x61, 0x6e, 0x79, 0x20, 0x73,
                0x74, 0x61, 0x74, 0x65, 0x6d, 0x65, 0x6e, 0x74, 0x20, 0x6d, 0x61, 0x64, 0x65, 0x20, 0x77, 0x69,
                0x74, 0x68, 0x69, 0x6e, 0x20, 0x74, 0x68, 0x65, 0x20, 0x63, 0x6f, 0x6e, 0x74, 0x65, 0x78, 0x74,
                0x20, 0x6f, 0x66, 0x20, 0x61, 0x6e, 0x20, 0x49, 0x45, 0x54, 0x46, 0x20, 0x61, 0x63, 0x74, 0x69,
                0x76, 0x69, 0x74, 0x79, 0x20, 0x69, 0x73, 0x20, 0x63, 0x6f, 0x6e, 0x73, 0x69, 0x64, 0x65, 0x72,
                0x65, 0x64, 0x20, 0x61, 0x6e, 0x20, 0x22, 0x49, 0x45, 0x54, 0x46, 0x20, 0x43, 0x6f, 0x6e, 0x74,
                0x72, 0x69, 0x62, 0x75, 0x74, 0x69, 0x6f, 0x6e, 0x22, 0x2e, 0x20, 0x53, 0x75, 0x63, 0x68, 0x20,
                0x73, 0x74, 0x61, 0x74, 0x65, 0x6d, 0x65, 0x6e, 0x74, 0x73, 0x20, 0x69, 0x6e, 0x63, 0x6c, 0x75,
                0x64, 0x65, 0x20, 0x6f, 0x72, 0x61, 0x6c, 0x20, 0x73, 0x74, 0x61, 0x74, 0x65, 0x6d, 0x65, 0x6e,
                0x74, 0x73, 0x20, 0x69, 0x6e, 0x20, 0x49, 0x45, 0x54, 0x46, 0x20, 0x73, 0x65, 0x73, 0x73, 0x69,
                0x6f, 0x6e, 0x73, 0x2c, 0x20, 0x61, 0x73, 0x20, 0x77, 0x65, 0x6c, 0x6c, 0x20, 0x61, 0x73, 0x20,
                0x77, 0x72, 0x69, 0x74, 0x74, 0x65, 0x6e, 0x20, 0x61, 0x6e, 0x64, 0x20, 0x65, 0x6c, 0x65, 0x63,
                0x74, 0x72, 0x6f, 0x6e, 0x69, 0x63, 0x20, 0x63, 0x6f, 0x6d, 0x6d, 0x75, 0x6e, 0x69, 0x63, 0x61,
                0x74, 0x69, 0x6f, 0x6e, 0x73, 0x20, 0x6d, 0x61, 0x64, 0x65, 0x20, 0x61, 0x74, 0x20, 0x61, 0x6e,
                0x79, 0x20, 0x74, 0x69, 0x6d, 0x65, 0x20, 0x6f, 0x72, 0x20, 0x70, 0x6c, 0x61, 0x63, 0x65, 0x2c,
                0x20, 0x77, 0x68, 0x69, 0x63, 0x68, 0x20, 0x61, 0x72, 0x65, 0x20, 0x61, 0x64, 0x64, 0x72, 0x65,
                0x73, 0x73, 0x65, 0x64, 0x20, 0x74, 0x6f
            };
            byte[] tag = new byte[]
            {
                0xf3, 0x47, 0x7e, 0x7c, 0xd9, 0x54, 0x17, 0xaf, 0x89, 0xa6, 0xb8, 0x79, 0x4c, 0x31, 0x0c, 0xf0
            };

            TestTag(key, msg, tag);
        }

        [TestMethod]
        public void TestVector4()
        {
            byte[] key = new byte[]
            {
                0x1c, 0x92, 0x40, 0xa5, 0xeb, 0x55, 0xd3, 0x8a, 0xf3, 0x33, 0x88, 0x86, 0x04, 0xf6, 0xb5, 0xf0,
                0x47, 0x39, 0x17, 0xc1, 0x40, 0x2b, 0x80, 0x09, 0x9d, 0xca, 0x5c, 0xbc, 0x20, 0x70, 0x75, 0xc0
            };
            byte[] msg = new byte[]
            {
                0x27, 0x54, 0x77, 0x61, 0x73, 0x20, 0x62, 0x72, 0x69, 0x6c, 0x6c, 0x69, 0x67, 0x2c, 0x20, 0x61,
                0x6e, 0x64, 0x20, 0x74, 0x68, 0x65, 0x20, 0x73, 0x6c, 0x69, 0x74, 0x68, 0x79, 0x20, 0x74, 0x6f,
                0x76, 0x65, 0x73, 0x0a, 0x44, 0x69, 0x64, 0x20, 0x67, 0x79, 0x72, 0x65, 0x20, 0x61, 0x6e, 0x64,
                0x20, 0x67, 0x69, 0x6d, 0x62, 0x6c, 0x65, 0x20, 0x69, 0x6e, 0x20, 0x74, 0x68, 0x65, 0x20, 0x77,
                0x61, 0x62, 0x65, 0x3a, 0x0a, 0x41, 0x6c, 0x6c, 0x20, 0x6d, 0x69, 0x6d, 0x73, 0x79, 0x20, 0x77,
                0x65, 0x72, 0x65, 0x20, 0x74, 0x68, 0x65, 0x20, 0x62, 0x6f, 0x72, 0x6f, 0x67, 0x6f, 0x76, 0x65,
                0x73, 0x2c, 0x0a, 0x41, 0x6e, 0x64, 0x20, 0x74, 0x68, 0x65, 0x20, 0x6d, 0x6f, 0x6d, 0x65, 0x20,
                0x72, 0x61, 0x74, 0x68, 0x73, 0x20, 0x6f, 0x75, 0x74, 0x67, 0x72, 0x61, 0x62, 0x65, 0x2e
            };
            byte[] tag = new byte[]
            {
                0x45, 0x41, 0x66, 0x9a, 0x7e, 0xaa, 0xee, 0x61, 0xe7, 0x08, 0xdc, 0x7c, 0xbc, 0xc5, 0xeb, 0x62
            };

            TestTag(key, msg, tag);
        }

        [TestMethod]
        public void TestVector5()
        {
            byte[] key = new byte[]
            {
                0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };
            byte[] msg = new byte[]
            {
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF
            };
            byte[] tag = new byte[]
            {
                0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };

            TestTag(key, msg, tag);
        }

        [TestMethod]
        public void TestVector6()
        {
            byte[] key = new byte[]
            {
                0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF
            };
            byte[] msg = new byte[]
            {
                0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };
            byte[] tag = new byte[]
            {
                0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };

            TestTag(key, msg, tag);
        }

        [TestMethod]
        public void TestVector7()
        {
            byte[] key = new byte[]
            {
                0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };
            byte[] msg = new byte[]
            {
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xF0, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0x11, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };
            byte[] tag = new byte[]
            {
                0x05, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };

            TestTag(key, msg, tag);
        }

        [TestMethod]
        public void TestVector8()
        {
            byte[] key = new byte[]
            {
                0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };
            byte[] msg = new byte[]
            {
                0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF,
                0xFB, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE, 0xFE,
                0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x01
            };
            byte[] tag = new byte[]
            {
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };

            TestTag(key, msg, tag);
        }

        [TestMethod]
        public void TestVector9()
        {
            byte[] key = new byte[]
            {
                0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };
            byte[] msg = new byte[]
            {
                0xFD, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF
            };
            byte[] tag = new byte[]
            {
                0xFA, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF
            };

            TestTag(key, msg, tag);
        }

        [TestMethod]
        public void TestVector10()
        {
            byte[] key = new byte[]
            {
                0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };
            byte[] msg = new byte[]
            {
                0xE3, 0x35, 0x94, 0xD7, 0x50, 0x5E, 0x43, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x33, 0x94, 0xD7, 0x50, 0x5E, 0x43, 0x79, 0xCD, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };
            byte[] tag = new byte[]
            {
                0x14, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x55, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };

            TestTag(key, msg, tag);
        }

        [TestMethod]
        public void TestVector11()
        {
            byte[] key = new byte[]
            {
                0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };
            byte[] msg = new byte[]
            {
                0xE3, 0x35, 0x94, 0xD7, 0x50, 0x5E, 0x43, 0xB9, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x33, 0x94, 0xD7, 0x50, 0x5E, 0x43, 0x79, 0xCD, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };
            byte[] tag = new byte[]
            {
                0x13, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };

            TestTag(key, msg, tag);
        }

        private static void TestTag(byte[] key, byte[] msg, byte[] tag)
        {
            var poly1305 = new Poly1305Function();
            poly1305.Init(key);
            poly1305.Write(msg, 0, msg.Length);
            var result = poly1305.ComputeResult();

            Assert.IsTrue(result.SequenceEqual(tag));
        }
    }
}