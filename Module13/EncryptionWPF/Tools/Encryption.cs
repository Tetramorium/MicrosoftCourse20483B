using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.IO;

namespace EncryptionWPF.Tools
{
    public static class Encryption
    {
        private static byte[] rgbKey;
        private static byte[] rgbIV;

        private static byte[] assymKeyWithPrivate;
        private static byte[] assymKeyWithoutPrivate;

        private static AesManaged algorithm = new AesManaged();

        private const string HashKey = "abc123";

        public static void Setup(string _Password, byte[] _Salt)
        {
            Rfc2898DeriveBytes rgb = new Rfc2898DeriveBytes(_Password, _Salt);

            algorithm.Padding = PaddingMode.PKCS7;

            rgbKey = rgb.GetBytes(algorithm.KeySize / 8);
            rgbIV = rgb.GetBytes(algorithm.BlockSize / 8);

            using (RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider())
            {
                // The receiver needs the private key to decrypt

                assymKeyWithPrivate = rsaProvider.ExportCspBlob(true);

                // Provider does not need the private key
                assymKeyWithoutPrivate = rsaProvider.ExportCspBlob(false);
            }
        }

        public static byte[] SymmetricEncryption(byte[] _Data)
        {
            byte[] encryptedData;

            using (MemoryStream bufferStream = new MemoryStream())
            {
                ICryptoTransform algorithmEncryptor = algorithm.CreateEncryptor(rgbKey, rgbIV);

                using (CryptoStream cryptoStream = new CryptoStream(bufferStream, algorithmEncryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(_Data, 0, _Data.Length);

                    cryptoStream.FlushFinalBlock();

                    cryptoStream.Close();
                    bufferStream.Close();
                }

                encryptedData = bufferStream.ToArray();
            }

            return encryptedData;
        }

        public static string SymmetricDecryption(byte[] _EncryptedData)
        {
            string data;

            AesManaged algorithm = new AesManaged();

            using (MemoryStream bufferStream = new MemoryStream(_EncryptedData))
            {
                ICryptoTransform algorithmDecryptor = algorithm.CreateDecryptor(rgbKey, rgbIV);

                using (CryptoStream cryptoStream = new CryptoStream(bufferStream, algorithmDecryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        data = streamReader.ReadToEnd();
                    }
                }
            }

            return data;
        }

        public static byte[] GenerateHash(byte[] _DataToHash)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(HashKey); ;

            using (HMACSHA512 hashAlgorithm = new HMACSHA512(bytes))
            {
                using (MemoryStream bufferStream = new MemoryStream(_DataToHash))
                {
                    return hashAlgorithm.ComputeHash(bufferStream);
                }
            }
        }

        public static byte[] AsymmetricEncryption(byte[] _Data)
        {
            using (RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider())
            {
                bool useOaePadding = true;

                rsaProvider.ImportCspBlob(assymKeyWithoutPrivate);

                return rsaProvider.Encrypt(_Data, useOaePadding);
            }
        }

        public static byte[] AsymmetricDecryption(byte[] _Data)
        {
            using (RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider())
            {
                bool useOaePadding = true;

                rsaProvider.ImportCspBlob(assymKeyWithPrivate);

                return rsaProvider.Decrypt(_Data, useOaePadding);
            }
        }

        // https://codereview.stackexchange.com/a/93622
        // Random salt Generator

        public static byte[] GetSalt(int maximumSaltLength)
        {
            var salt = new byte[maximumSaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }
    }
}
