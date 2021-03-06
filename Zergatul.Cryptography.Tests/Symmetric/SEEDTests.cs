﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zergatul.Cryptography.Symmetric;

namespace Zergatul.Cryptography.Tests.BlockCipher
{
    [TestClass]
    public class SEEDTests
    {
        [TestMethod]
        public void SEED_Vector1()
        {
            TestEncryptDecrypt(
                "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00",
                "00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F",
                "5E BA C6 E0 05 4E 16 68 19 AF F1 CC 6D 34 6C DB");
        }

        [TestMethod]
        public void SEED_Vector2()
        {
            TestEncryptDecrypt(
                "00 01 02 03 04 05 06 07 08 09 0A 0B 0C 0D 0E 0F",
                "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00",
                "C1 1F 22 F2 01 40 50 50 84 48 35 97 E4 37 0F 43");
        }

        [TestMethod]
        public void SEED_Vector3()
        {
            TestEncryptDecrypt(
                "47 06 48 08 51 E6 1B E8 5D 74 BF B3 FD 95 61 85",
                "83 A2 F8 A2 88 64 1F B9 A4 E9 A5 CC 2F 13 1C 7D",
                "EE 54 D1 3E BC AE 70 6D 22 6B C3 14 2C D4 0D 4A");
        }

        [TestMethod]
        public void SEED_Vector4()
        {
            TestEncryptDecrypt(
                "28 DB C3 BC 49 FF D8 7D CF A5 09 B1 1D 42 2B E7",
                "B4 1E 6B E2 EB A8 4A 14 8E 2E ED 84 59 3C 5E C7",
                "9B 9B 7B FC D1 81 3C B9 5D 0B 36 18 F4 0F 51 22");
        }

        private static void TestEncryptDecrypt(string key, string plaintext, string ciphertext)
        {
            byte[] bkey = BitHelper.HexToBytes(key.Replace(" ", ""));
            byte[] bplain = BitHelper.HexToBytes(plaintext.Replace(" ", ""));
            byte[] bcipher = BitHelper.HexToBytes(ciphertext.Replace(" ", ""));

            var seed = new SEED();

            var enc = seed.CreateEncryptor(bkey);
            Assert.IsTrue(bcipher.SequenceEqual(enc(bplain)));

            var dec = seed.CreateDecryptor(bkey);
            Assert.IsTrue(bplain.SequenceEqual(dec(bcipher)));
        }
    }
}