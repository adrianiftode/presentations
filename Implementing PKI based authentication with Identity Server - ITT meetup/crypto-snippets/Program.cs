using System;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;
using System.Text;

namespace CryptoSnippets
{
    class Program
    {
        static void Main(string[] args)
        {
            AliceAndBobConfidentiallyChangeMessagesUsingAes();
        }

        private static void AliceAndBobConfidentiallyChangeMessagesUsingAes()
        {
            var key = Random.GenerateRandomNumber(32); // share it with Bob
            Console.WriteLine("Shared key {0}", key.ToBase64());
            var message = "Ted and Carole went outside. Don't tell.";

            var (cipher, iv) = Aes.Encrypt(message.ToBytes(), key);
            var decrypted = Aes.Decrypt(cipher, key, iv);

            // Copy and and send the cypher and the iv to Bob so he can decrypt them
            Console.WriteLine("Used initialization vector {0}", iv.ToBase64());
            Console.WriteLine("Cipher {0}", cipher.ToBase64());

            Console.WriteLine(decrypted.FromBytes());
        }

        public static class Random
        {
            public static byte[] GenerateRandomNumber(int length)
            {
                using var randomNumberGenerator = new RNGCryptoServiceProvider();
                var randomNumber = new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);
                return randomNumber;
            }
        }

        public static class Aes
        {
            public static (byte[] cypher, byte[] iv) Encrypt(byte[] dataToEncrypt, byte[] key)
            {
                using var aes = new AesCryptoServiceProvider
                {
                    Key = key,
                };
                aes.GenerateIV();

                using var memoryStream = new MemoryStream();
                var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                cryptoStream.FlushFinalBlock();

                return (memoryStream.ToArray(), aes.IV);
            }

            public static byte[] Decrypt(byte[] dataToDecrypt, byte[] key, byte[] iv)
            {
                using var aes = new AesCryptoServiceProvider
                {
                    Key = key,
                    IV = iv
                };

                using var memoryStream = new MemoryStream();
                var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(dataToDecrypt, 0, dataToDecrypt.Length);
                cryptoStream.FlushFinalBlock();

                return memoryStream.ToArray();
            }
        }
    }

    public static class CryptoExtensions
    {
        public static string ToBase64(this byte[] source) => Convert.ToBase64String(source);
        public static byte[] FromBase64(this string source) => Convert.FromBase64String(source);
        public static byte[] ToBytes(this string source) => Encoding.UTF8.GetBytes(source);
        public static string FromBytes(this byte[] source) => Encoding.UTF8.GetString(source);
    }
}
