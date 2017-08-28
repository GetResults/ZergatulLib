﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zergatul.Cryptography;
using Zergatul.Cryptography.Asymmetric;
using Zergatul.Math.EllipticCurves;
using Zergatul.Network.Tls.Messages;

namespace Zergatul.Network.Tls
{
    internal static class ECDHERoutine
    {
        public static ECDiffieHellman GenerateServerKeyExchange(ServerKeyExchange message, ISecureRandom random, TlsStreamSettings settings)
        {
            var namedCurve = NamedGroup.secp256r1;

            var ecdh = new ECDiffieHellman();
            ecdh.Random = random;
            ecdh.Parameters = ResolveCurve(namedCurve);
            ecdh.GenerateKeys();

            message.ECParams = new ServerECDHParams
            {
                CurveParams = new ECParameters
                {
                    CurveType = ECCurveType.NamedCurve,
                    NamedCurve = namedCurve
                },
                Point = ecdh.PublicKey.ToBytes()
            };

            return ecdh;
        }

        public static void ReadServerKeyExchange(ServerKeyExchange message, BinaryReader reader)
        {
            message.ECParams = new ServerECDHParams();
            message.ECParams.Read(reader);
        }

        public static ECDiffieHellman GetSharedSecretAsClient(ServerKeyExchange message, ISecureRandom random)
        {
            var ecdh = new ECDiffieHellman();
            ecdh.Random = random;
            ecdh.Parameters = ResolveCurve(message.ECParams.CurveParams.NamedCurve);
            ecdh.GenerateKeys();
            ecdh.KeyExchange.CalculateSharedSecret(ECPointGeneric.Parse(message.ECParams.Point, ecdh.Parameters));
            return ecdh;
        }

        public static void WriteServerKeyExchange(ServerKeyExchange message, BinaryWriter writer)
        {
            writer.WriteBytes(message.ECParams.ToBytes());
        }

        public static byte[] GenerateClientKeyExchange(ClientKeyExchange message, ECDiffieHellman ecdh)
        {
            message.ECDH_Yc = ecdh.PublicKey.ToBytes();
            return ecdh.KeyExchange.SharedSecret.XToBytes();
        }

        public static byte[] ReadClientKeyExchange(ClientKeyExchange message, BinaryReader reader, ECDiffieHellman ecdh)
        {
            message.ECDH_Yc = reader.ReadBytes(reader.ReadByte());

            ecdh.KeyExchange.CalculateSharedSecret(ECPointGeneric.Parse(message.ECDH_Yc, ecdh.Parameters));

            /*
                All ECDH calculations (including parameter and key generation as well
                as the shared secret calculation) are performed according to [6]
                using the ECKAS-DH1 scheme with the identity map as key derivation
                function (KDF), so that the premaster secret is the x-coordinate of
                the ECDH shared secret elliptic curve point represented as an octet
                string.  Note that this octet string (Z in IEEE 1363 terminology) as
                output by FE2OSP, the Field Element to Octet String Conversion
                Primitive, has constant length for any given field; leading zeros
                found in this octet string MUST NOT be truncated.
            */
            return ecdh.KeyExchange.SharedSecret.XToBytes();
        }

        public static void WriteClientKeyExchange(ClientKeyExchange message, BinaryWriter writer)
        {
            writer.WriteByte((byte)message.ECDH_Yc.Length);
            writer.WriteBytes(message.ECDH_Yc);
        }

        public static IEllipticCurve ResolveCurve(NamedGroup curve)
        {
            switch (curve)
            {
                case NamedGroup.secp160k1: return Math.EllipticCurves.PrimeField.EllipticCurve.secp160k1;
                case NamedGroup.secp160r1: return Math.EllipticCurves.PrimeField.EllipticCurve.secp160r1;
                case NamedGroup.secp160r2: return Math.EllipticCurves.PrimeField.EllipticCurve.secp160r2;
                case NamedGroup.secp192k1: return Math.EllipticCurves.PrimeField.EllipticCurve.secp192k1;
                case NamedGroup.secp192r1: return Math.EllipticCurves.PrimeField.EllipticCurve.secp192r1;
                case NamedGroup.secp224k1: return Math.EllipticCurves.PrimeField.EllipticCurve.secp224k1;
                case NamedGroup.secp224r1: return Math.EllipticCurves.PrimeField.EllipticCurve.secp224r1;
                case NamedGroup.secp256k1: return Math.EllipticCurves.PrimeField.EllipticCurve.secp256k1;
                case NamedGroup.secp256r1: return Math.EllipticCurves.PrimeField.EllipticCurve.secp256r1;
                case NamedGroup.secp384r1: return Math.EllipticCurves.PrimeField.EllipticCurve.secp384r1;
                case NamedGroup.secp521r1: return Math.EllipticCurves.PrimeField.EllipticCurve.secp521r1;

                case NamedGroup.sect163k1: return Math.EllipticCurves.BinaryField.EllipticCurve.sect163k1;
                case NamedGroup.sect163r1: return Math.EllipticCurves.BinaryField.EllipticCurve.sect163r1;
                case NamedGroup.sect163r2: return Math.EllipticCurves.BinaryField.EllipticCurve.sect163r2;
                case NamedGroup.sect193r1: return Math.EllipticCurves.BinaryField.EllipticCurve.sect193r1;
                case NamedGroup.sect193r2: return Math.EllipticCurves.BinaryField.EllipticCurve.sect193r2;
                case NamedGroup.sect233k1: return Math.EllipticCurves.BinaryField.EllipticCurve.sect233k1;
                case NamedGroup.sect233r1: return Math.EllipticCurves.BinaryField.EllipticCurve.sect233r1;
                case NamedGroup.sect239k1: return Math.EllipticCurves.BinaryField.EllipticCurve.sect239k1;
                case NamedGroup.sect283k1: return Math.EllipticCurves.BinaryField.EllipticCurve.sect283k1;
                case NamedGroup.sect283r1: return Math.EllipticCurves.BinaryField.EllipticCurve.sect283r1;
                case NamedGroup.sect409k1: return Math.EllipticCurves.BinaryField.EllipticCurve.sect409k1;
                case NamedGroup.sect409r1: return Math.EllipticCurves.BinaryField.EllipticCurve.sect409r1;
                case NamedGroup.sect571k1: return Math.EllipticCurves.BinaryField.EllipticCurve.sect571k1;
                case NamedGroup.sect571r1: return Math.EllipticCurves.BinaryField.EllipticCurve.sect571r1;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}