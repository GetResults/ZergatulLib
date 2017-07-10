﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zergatul.Math;

namespace Zergatul.Cryptography.Asymmetric
{
    public class DiffieHellmanParameters
    {
        /// <summary>
        /// Generator
        /// </summary>
        public BigInteger g { get; private set; }

        /// <summary>
        /// Prime
        /// </summary>
        public BigInteger p { get; private set; }

        public DiffieHellmanParameters(BigInteger g, BigInteger p)
        {
            this.g = g;
            this.p = p;
        }

        /// <summary>
        /// RFC 2409, key size - 768 bits
        /// </summary>
        public static DiffieHellmanParameters Group1 = new DiffieHellmanParameters(
            new BigInteger(2),
            new BigInteger(new uint[]
            {
                0xFFFFFFFF, 0xFFFFFFFF, 0xC90FDAA2, 0x2168C234, 0xC4C6628B, 0x80DC1CD1,
                0x29024E08, 0x8A67CC74, 0x020BBEA6, 0x3B139B22, 0x514A0879, 0x8E3404DD,
                0xEF9519B3, 0xCD3A431B, 0x302B0A6D, 0xF25F1437, 0x4FE1356D, 0x6D51C245,
                0xE485B576, 0x625E7EC6, 0xF44C42E9, 0xA63A3620, 0xFFFFFFFF, 0xFFFFFFFF,
            }, ByteOrder.BigEndian));

        /// <summary>
        /// RFC 2409, key size - 1024 bits
        /// </summary>
        public static DiffieHellmanParameters Group2 = new DiffieHellmanParameters(
            new BigInteger(2),
            new BigInteger(new uint[]
            {
                0xFFFFFFFF, 0xFFFFFFFF, 0xC90FDAA2, 0x2168C234, 0xC4C6628B, 0x80DC1CD1,
                0x29024E08, 0x8A67CC74, 0x020BBEA6, 0x3B139B22, 0x514A0879, 0x8E3404DD,
                0xEF9519B3, 0xCD3A431B, 0x302B0A6D, 0xF25F1437, 0x4FE1356D, 0x6D51C245,
                0xE485B576, 0x625E7EC6, 0xF44C42E9, 0xA637ED6B, 0x0BFF5CB6, 0xF406B7ED,
                0xEE386BFB, 0x5A899FA5, 0xAE9F2411, 0x7C4B1FE6, 0x49286651, 0xECE65381,
                0xFFFFFFFF, 0xFFFFFFFF,
            }, ByteOrder.BigEndian));

        /// <summary>
        /// RFC 3526, key size - 1536 bits
        /// </summary>
        public static DiffieHellmanParameters Group5 = new DiffieHellmanParameters(
            new BigInteger(2),
            new BigInteger(new uint[]
            {
                0xFFFFFFFF, 0xFFFFFFFF, 0xC90FDAA2, 0x2168C234, 0xC4C6628B, 0x80DC1CD1,
                0x29024E08, 0x8A67CC74, 0x020BBEA6, 0x3B139B22, 0x514A0879, 0x8E3404DD,
                0xEF9519B3, 0xCD3A431B, 0x302B0A6D, 0xF25F1437, 0x4FE1356D, 0x6D51C245,
                0xE485B576, 0x625E7EC6, 0xF44C42E9, 0xA637ED6B, 0x0BFF5CB6, 0xF406B7ED,
                0xEE386BFB, 0x5A899FA5, 0xAE9F2411, 0x7C4B1FE6, 0x49286651, 0xECE45B3D,
                0xC2007CB8, 0xA163BF05, 0x98DA4836, 0x1C55D39A, 0x69163FA8, 0xFD24CF5F,
                0x83655D23, 0xDCA3AD96, 0x1C62F356, 0x208552BB, 0x9ED52907, 0x7096966D,
                0x670C354E, 0x4ABC9804, 0xF1746C08, 0xCA237327, 0xFFFFFFFF, 0xFFFFFFFF,
            }, ByteOrder.BigEndian));

        /// <summary>
        /// RFC 3526, key size - 2048 bits
        /// </summary>
        public static DiffieHellmanParameters Group14 = new DiffieHellmanParameters(
            new BigInteger(2),
            new BigInteger(new uint[]
            {
                0xFFFFFFFF, 0xFFFFFFFF, 0xC90FDAA2, 0x2168C234, 0xC4C6628B, 0x80DC1CD1,
                0x29024E08, 0x8A67CC74, 0x020BBEA6, 0x3B139B22, 0x514A0879, 0x8E3404DD,
                0xEF9519B3, 0xCD3A431B, 0x302B0A6D, 0xF25F1437, 0x4FE1356D, 0x6D51C245,
                0xE485B576, 0x625E7EC6, 0xF44C42E9, 0xA637ED6B, 0x0BFF5CB6, 0xF406B7ED,
                0xEE386BFB, 0x5A899FA5, 0xAE9F2411, 0x7C4B1FE6, 0x49286651, 0xECE45B3D,
                0xC2007CB8, 0xA163BF05, 0x98DA4836, 0x1C55D39A, 0x69163FA8, 0xFD24CF5F,
                0x83655D23, 0xDCA3AD96, 0x1C62F356, 0x208552BB, 0x9ED52907, 0x7096966D,
                0x670C354E, 0x4ABC9804, 0xF1746C08, 0xCA18217C, 0x32905E46, 0x2E36CE3B,
                0xE39E772C, 0x180E8603, 0x9B2783A2, 0xEC07A28F, 0xB5C55DF0, 0x6F4C52C9,
                0xDE2BCBF6, 0x95581718, 0x3995497C, 0xEA956AE5, 0x15D22618, 0x98FA0510,
                0x15728E5A, 0x8AACAA68, 0xFFFFFFFF, 0xFFFFFFFF,
            }, ByteOrder.BigEndian));

        /// <summary>
        /// RFC 3526, key size - 3072 bits
        /// </summary>
        public static DiffieHellmanParameters Group15 = new DiffieHellmanParameters(
            new BigInteger(2),
            new BigInteger(new uint[]
            {
                0xFFFFFFFF, 0xFFFFFFFF, 0xC90FDAA2, 0x2168C234, 0xC4C6628B, 0x80DC1CD1,
                0x29024E08, 0x8A67CC74, 0x020BBEA6, 0x3B139B22, 0x514A0879, 0x8E3404DD,
                0xEF9519B3, 0xCD3A431B, 0x302B0A6D, 0xF25F1437, 0x4FE1356D, 0x6D51C245,
                0xE485B576, 0x625E7EC6, 0xF44C42E9, 0xA637ED6B, 0x0BFF5CB6, 0xF406B7ED,
                0xEE386BFB, 0x5A899FA5, 0xAE9F2411, 0x7C4B1FE6, 0x49286651, 0xECE45B3D,
                0xC2007CB8, 0xA163BF05, 0x98DA4836, 0x1C55D39A, 0x69163FA8, 0xFD24CF5F,
                0x83655D23, 0xDCA3AD96, 0x1C62F356, 0x208552BB, 0x9ED52907, 0x7096966D,
                0x670C354E, 0x4ABC9804, 0xF1746C08, 0xCA18217C, 0x32905E46, 0x2E36CE3B,
                0xE39E772C, 0x180E8603, 0x9B2783A2, 0xEC07A28F, 0xB5C55DF0, 0x6F4C52C9,
                0xDE2BCBF6, 0x95581718, 0x3995497C, 0xEA956AE5, 0x15D22618, 0x98FA0510,
                0x15728E5A, 0x8AAAC42D, 0xAD33170D, 0x04507A33, 0xA85521AB, 0xDF1CBA64,
                0xECFB8504, 0x58DBEF0A, 0x8AEA7157, 0x5D060C7D, 0xB3970F85, 0xA6E1E4C7,
                0xABF5AE8C, 0xDB0933D7, 0x1E8C94E0, 0x4A25619D, 0xCEE3D226, 0x1AD2EE6B,
                0xF12FFA06, 0xD98A0864, 0xD8760273, 0x3EC86A64, 0x521F2B18, 0x177B200C,
                0xBBE11757, 0x7A615D6C, 0x770988C0, 0xBAD946E2, 0x08E24FA0, 0x74E5AB31,
                0x43DB5BFC, 0xE0FD108E, 0x4B82D120, 0xA93AD2CA, 0xFFFFFFFF, 0xFFFFFFFF,
            }, ByteOrder.BigEndian));

        /// <summary>
        /// RFC 3526, key size - 4096 bits
        /// </summary>
        public static DiffieHellmanParameters Group16 = new DiffieHellmanParameters(
            new BigInteger(2),
            new BigInteger(new uint[]
            {
                0xFFFFFFFF, 0xFFFFFFFF, 0xC90FDAA2, 0x2168C234, 0xC4C6628B, 0x80DC1CD1,
                0x29024E08, 0x8A67CC74, 0x020BBEA6, 0x3B139B22, 0x514A0879, 0x8E3404DD,
                0xEF9519B3, 0xCD3A431B, 0x302B0A6D, 0xF25F1437, 0x4FE1356D, 0x6D51C245,
                0xE485B576, 0x625E7EC6, 0xF44C42E9, 0xA637ED6B, 0x0BFF5CB6, 0xF406B7ED,
                0xEE386BFB, 0x5A899FA5, 0xAE9F2411, 0x7C4B1FE6, 0x49286651, 0xECE45B3D,
                0xC2007CB8, 0xA163BF05, 0x98DA4836, 0x1C55D39A, 0x69163FA8, 0xFD24CF5F,
                0x83655D23, 0xDCA3AD96, 0x1C62F356, 0x208552BB, 0x9ED52907, 0x7096966D,
                0x670C354E, 0x4ABC9804, 0xF1746C08, 0xCA18217C, 0x32905E46, 0x2E36CE3B,
                0xE39E772C, 0x180E8603, 0x9B2783A2, 0xEC07A28F, 0xB5C55DF0, 0x6F4C52C9,
                0xDE2BCBF6, 0x95581718, 0x3995497C, 0xEA956AE5, 0x15D22618, 0x98FA0510,
                0x15728E5A, 0x8AAAC42D, 0xAD33170D, 0x04507A33, 0xA85521AB, 0xDF1CBA64,
                0xECFB8504, 0x58DBEF0A, 0x8AEA7157, 0x5D060C7D, 0xB3970F85, 0xA6E1E4C7,
                0xABF5AE8C, 0xDB0933D7, 0x1E8C94E0, 0x4A25619D, 0xCEE3D226, 0x1AD2EE6B,
                0xF12FFA06, 0xD98A0864, 0xD8760273, 0x3EC86A64, 0x521F2B18, 0x177B200C,
                0xBBE11757, 0x7A615D6C, 0x770988C0, 0xBAD946E2, 0x08E24FA0, 0x74E5AB31,
                0x43DB5BFC, 0xE0FD108E, 0x4B82D120, 0xA9210801, 0x1A723C12, 0xA787E6D7,
                0x88719A10, 0xBDBA5B26, 0x99C32718, 0x6AF4E23C, 0x1A946834, 0xB6150BDA,
                0x2583E9CA, 0x2AD44CE8, 0xDBBBC2DB, 0x04DE8EF9, 0x2E8EFC14, 0x1FBECAA6,
                0x287C5947, 0x4E6BC05D, 0x99B2964F, 0xA090C3A2, 0x233BA186, 0x515BE7ED,
                0x1F612970, 0xCEE2D7AF, 0xB81BDD76, 0x2170481C, 0xD0069127, 0xD5B05AA9,
                0x93B4EA98, 0x8D8FDDC1, 0x86FFB7DC, 0x90A6C08F, 0x4DF435C9, 0x34063199,
                0xFFFFFFFF, 0xFFFFFFFF,
            }, ByteOrder.BigEndian));

        /// <summary>
        /// RFC 3526, key size - 6144 bits
        /// </summary>
        public static DiffieHellmanParameters Group17 = new DiffieHellmanParameters(
            new BigInteger(2),
            new BigInteger(new uint[]
            {
                0xFFFFFFFF, 0xFFFFFFFF, 0xC90FDAA2, 0x2168C234, 0xC4C6628B, 0x80DC1CD1, 0x29024E08,
                0x8A67CC74, 0x020BBEA6, 0x3B139B22, 0x514A0879, 0x8E3404DD, 0xEF9519B3, 0xCD3A431B,
                0x302B0A6D, 0xF25F1437, 0x4FE1356D, 0x6D51C245, 0xE485B576, 0x625E7EC6, 0xF44C42E9,
                0xA637ED6B, 0x0BFF5CB6, 0xF406B7ED, 0xEE386BFB, 0x5A899FA5, 0xAE9F2411, 0x7C4B1FE6,
                0x49286651, 0xECE45B3D, 0xC2007CB8, 0xA163BF05, 0x98DA4836, 0x1C55D39A, 0x69163FA8,
                0xFD24CF5F, 0x83655D23, 0xDCA3AD96, 0x1C62F356, 0x208552BB, 0x9ED52907, 0x7096966D,
                0x670C354E, 0x4ABC9804, 0xF1746C08, 0xCA18217C, 0x32905E46, 0x2E36CE3B, 0xE39E772C,
                0x180E8603, 0x9B2783A2, 0xEC07A28F, 0xB5C55DF0, 0x6F4C52C9, 0xDE2BCBF6, 0x95581718,
                0x3995497C, 0xEA956AE5, 0x15D22618, 0x98FA0510, 0x15728E5A, 0x8AAAC42D, 0xAD33170D,
                0x04507A33, 0xA85521AB, 0xDF1CBA64, 0xECFB8504, 0x58DBEF0A, 0x8AEA7157, 0x5D060C7D,
                0xB3970F85, 0xA6E1E4C7, 0xABF5AE8C, 0xDB0933D7, 0x1E8C94E0, 0x4A25619D, 0xCEE3D226,
                0x1AD2EE6B, 0xF12FFA06, 0xD98A0864, 0xD8760273, 0x3EC86A64, 0x521F2B18, 0x177B200C,
                0xBBE11757, 0x7A615D6C, 0x770988C0, 0xBAD946E2, 0x08E24FA0, 0x74E5AB31, 0x43DB5BFC,
                0xE0FD108E, 0x4B82D120, 0xA9210801, 0x1A723C12, 0xA787E6D7, 0x88719A10, 0xBDBA5B26,
                0x99C32718, 0x6AF4E23C, 0x1A946834, 0xB6150BDA, 0x2583E9CA, 0x2AD44CE8, 0xDBBBC2DB,
                0x04DE8EF9, 0x2E8EFC14, 0x1FBECAA6, 0x287C5947, 0x4E6BC05D, 0x99B2964F, 0xA090C3A2,
                0x233BA186, 0x515BE7ED, 0x1F612970, 0xCEE2D7AF, 0xB81BDD76, 0x2170481C, 0xD0069127,
                0xD5B05AA9, 0x93B4EA98, 0x8D8FDDC1, 0x86FFB7DC, 0x90A6C08F, 0x4DF435C9, 0x34028492,
                0x36C3FAB4, 0xD27C7026, 0xC1D4DCB2, 0x602646DE, 0xC9751E76, 0x3DBA37BD, 0xF8FF9406,
                0xAD9E530E, 0xE5DB382F, 0x413001AE, 0xB06A53ED, 0x9027D831, 0x179727B0, 0x865A8918,
                0xDA3EDBEB, 0xCF9B14ED, 0x44CE6CBA, 0xCED4BB1B, 0xDB7F1447, 0xE6CC254B, 0x33205151,
                0x2BD7AF42, 0x6FB8F401, 0x378CD2BF, 0x5983CA01, 0xC64B92EC, 0xF032EA15, 0xD1721D03,
                0xF482D7CE, 0x6E74FEF6, 0xD55E702F, 0x46980C82, 0xB5A84031, 0x900B1C9E, 0x59E7C97F,
                0xBEC7E8F3, 0x23A97A7E, 0x36CC88BE, 0x0F1D45B7, 0xFF585AC5, 0x4BD407B2, 0x2B4154AA,
                0xCC8F6D7E, 0xBF48E1D8, 0x14CC5ED2, 0x0F8037E0, 0xA79715EE, 0xF29BE328, 0x06A1D58B,
                0xB7C5DA76, 0xF550AA3D, 0x8A1FBFF0, 0xEB19CCB1, 0xA313D55C, 0xDA56C9EC, 0x2EF29632,
                0x387FE8D7, 0x6E3C0468, 0x043E8F66, 0x3F4860EE, 0x12BF2D5B, 0x0B7474D6, 0xE694F91E,
                0x6DCC4024, 0xFFFFFFFF, 0xFFFFFFFF,
            }, ByteOrder.BigEndian));

        /// <summary>
        /// RFC 3526, key size - 8192 bits
        /// </summary>
        public static DiffieHellmanParameters Group18 = new DiffieHellmanParameters(
            new BigInteger(2),
            new BigInteger(new uint[]
            {
                0xFFFFFFFF, 0xFFFFFFFF, 0xC90FDAA2, 0x2168C234, 0xC4C6628B, 0x80DC1CD1,
                0x29024E08, 0x8A67CC74, 0x020BBEA6, 0x3B139B22, 0x514A0879, 0x8E3404DD,
                0xEF9519B3, 0xCD3A431B, 0x302B0A6D, 0xF25F1437, 0x4FE1356D, 0x6D51C245,
                0xE485B576, 0x625E7EC6, 0xF44C42E9, 0xA637ED6B, 0x0BFF5CB6, 0xF406B7ED,
                0xEE386BFB, 0x5A899FA5, 0xAE9F2411, 0x7C4B1FE6, 0x49286651, 0xECE45B3D,
                0xC2007CB8, 0xA163BF05, 0x98DA4836, 0x1C55D39A, 0x69163FA8, 0xFD24CF5F,
                0x83655D23, 0xDCA3AD96, 0x1C62F356, 0x208552BB, 0x9ED52907, 0x7096966D,
                0x670C354E, 0x4ABC9804, 0xF1746C08, 0xCA18217C, 0x32905E46, 0x2E36CE3B,
                0xE39E772C, 0x180E8603, 0x9B2783A2, 0xEC07A28F, 0xB5C55DF0, 0x6F4C52C9,
                0xDE2BCBF6, 0x95581718, 0x3995497C, 0xEA956AE5, 0x15D22618, 0x98FA0510,
                0x15728E5A, 0x8AAAC42D, 0xAD33170D, 0x04507A33, 0xA85521AB, 0xDF1CBA64,
                0xECFB8504, 0x58DBEF0A, 0x8AEA7157, 0x5D060C7D, 0xB3970F85, 0xA6E1E4C7,
                0xABF5AE8C, 0xDB0933D7, 0x1E8C94E0, 0x4A25619D, 0xCEE3D226, 0x1AD2EE6B,
                0xF12FFA06, 0xD98A0864, 0xD8760273, 0x3EC86A64, 0x521F2B18, 0x177B200C,
                0xBBE11757, 0x7A615D6C, 0x770988C0, 0xBAD946E2, 0x08E24FA0, 0x74E5AB31,
                0x43DB5BFC, 0xE0FD108E, 0x4B82D120, 0xA9210801, 0x1A723C12, 0xA787E6D7,
                0x88719A10, 0xBDBA5B26, 0x99C32718, 0x6AF4E23C, 0x1A946834, 0xB6150BDA,
                0x2583E9CA, 0x2AD44CE8, 0xDBBBC2DB, 0x04DE8EF9, 0x2E8EFC14, 0x1FBECAA6,
                0x287C5947, 0x4E6BC05D, 0x99B2964F, 0xA090C3A2, 0x233BA186, 0x515BE7ED,
                0x1F612970, 0xCEE2D7AF, 0xB81BDD76, 0x2170481C, 0xD0069127, 0xD5B05AA9,
                0x93B4EA98, 0x8D8FDDC1, 0x86FFB7DC, 0x90A6C08F, 0x4DF435C9, 0x34028492,
                0x36C3FAB4, 0xD27C7026, 0xC1D4DCB2, 0x602646DE, 0xC9751E76, 0x3DBA37BD,
                0xF8FF9406, 0xAD9E530E, 0xE5DB382F, 0x413001AE, 0xB06A53ED, 0x9027D831,
                0x179727B0, 0x865A8918, 0xDA3EDBEB, 0xCF9B14ED, 0x44CE6CBA, 0xCED4BB1B,
                0xDB7F1447, 0xE6CC254B, 0x33205151, 0x2BD7AF42, 0x6FB8F401, 0x378CD2BF,
                0x5983CA01, 0xC64B92EC, 0xF032EA15, 0xD1721D03, 0xF482D7CE, 0x6E74FEF6,
                0xD55E702F, 0x46980C82, 0xB5A84031, 0x900B1C9E, 0x59E7C97F, 0xBEC7E8F3,
                0x23A97A7E, 0x36CC88BE, 0x0F1D45B7, 0xFF585AC5, 0x4BD407B2, 0x2B4154AA,
                0xCC8F6D7E, 0xBF48E1D8, 0x14CC5ED2, 0x0F8037E0, 0xA79715EE, 0xF29BE328,
                0x06A1D58B, 0xB7C5DA76, 0xF550AA3D, 0x8A1FBFF0, 0xEB19CCB1, 0xA313D55C,
                0xDA56C9EC, 0x2EF29632, 0x387FE8D7, 0x6E3C0468, 0x043E8F66, 0x3F4860EE,
                0x12BF2D5B, 0x0B7474D6, 0xE694F91E, 0x6DBE1159, 0x74A3926F, 0x12FEE5E4,
                0x38777CB6, 0xA932DF8C, 0xD8BEC4D0, 0x73B931BA, 0x3BC832B6, 0x8D9DD300,
                0x741FA7BF, 0x8AFC47ED, 0x2576F693, 0x6BA42466, 0x3AAB639C, 0x5AE4F568,
                0x3423B474, 0x2BF1C978, 0x238F16CB, 0xE39D652D, 0xE3FDB8BE, 0xFC848AD9,
                0x22222E04, 0xA4037C07, 0x13EB57A8, 0x1A23F0C7, 0x3473FC64, 0x6CEA306B,
                0x4BCBC886, 0x2F8385DD, 0xFA9D4B7F, 0xA2C087E8, 0x79683303, 0xED5BDD3A,
                0x062B3CF5, 0xB3A278A6, 0x6D2A13F8, 0x3F44F82D, 0xDF310EE0, 0x74AB6A36,
                0x4597E899, 0xA0255DC1, 0x64F31CC5, 0x0846851D, 0xF9AB4819, 0x5DED7EA1,
                0xB1D510BD, 0x7EE74D73, 0xFAF36BC3, 0x1ECFA268, 0x359046F4, 0xEB879F92,
                0x4009438B, 0x481C6CD7, 0x889A002E, 0xD5EE382B, 0xC9190DA6, 0xFC026E47,
                0x9558E447, 0x5677E9AA, 0x9E3050E2, 0x765694DF, 0xC81F56E8, 0x80B96E71,
                0x60C980DD, 0x98EDD3DF, 0xFFFFFFFF, 0xFFFFFFFF,
            }, ByteOrder.BigEndian));
    }
}