using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace exam483.chapter3._Debug._Security
{
    class Skill2_Encryption
    {
        /*
         * Расширенный стандарт шифрования (AES) используется во всем мире для шифрования данных.
         * Он заменяет стандарт шифрования данных (DES). Это симметричная система шифрования.
         * 
         * DES, RC2 больше не безопасны!
         * 
         * Rijndael - это шифр, на котором основывался AES. AES реализован как подмножество Rijndael.
         * TripleDES повышает безопасность стандарта DES, шифруя входящие данные три раза подряд,
         * используя три разных ключа. Индустрия электронных платежей использует TripleDES.
         * 
         * Для каждого пользователя компьютера предусмотрено безопасное хранилище ключей - CspParameters.KeyContainerName
         * Удалить хранилища - rsaStore.PersistKeyInCsp = false; rsaStore.Clear ();
         *
         * Сохранить на уровне машины! -
         *          cspParams.Flags = CspProviderFlags.UseMachineKeyStore;
         *          provider.PersistKeyInCsp = true;
         *          provider.Clear();
         *
         * Хэши - MD5, SHA1, SHA2...
         * CryptoStream можно пихнуть в криптострим и зашифровать другим aes
         */

        void Dump(string name, byte[] bytes)
        {
            Console.WriteLine("{0}: " + string.Join(' ', bytes.Select(x => x.ToString("X"))), name);
        }

        public void Do()
        {
            // AES();
            // RSA();
            // KeyStorage();
        }

        private void KeyStorage()
        {
            string containerName = "StoreBitch";
            CspParameters csp = new CspParameters()
            {
                KeyContainerName = containerName
            };
            var provider = new RSACryptoServiceProvider(csp);
            Console.WriteLine(provider.ToXmlString(false)); //Значение навсегда тут для 1 пользователя
        }

        #region RSA
        private void RSA()
        {
            string plainText = "This is my super duper secret key!";
            byte[] bytes = Encoding.ASCII.GetBytes(plainText);
            Dump("text", bytes);

            byte[] encrypted;

            //Шифрование
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            string publicKey = provider.ToXmlString(false);   //
            Console.WriteLine("public: {0}", publicKey);      //
            string privateKey = provider.ToXmlString(true);   // ключики
            Console.WriteLine("private: {0}", privateKey);    //

            encrypted = EncryptRSA(bytes, provider, publicKey);

            //Дешифровка
            DecryptRSA(encrypted, privateKey);
        }

        private byte[] EncryptRSA(byte[] bytes, RSACryptoServiceProvider provider, string publicKey)
        {
            byte[] encrypted;
            provider.FromXmlString(publicKey);
            encrypted = provider.Encrypt(bytes, false);
            Dump("encrypted", encrypted);
            return encrypted;
        }

        private void DecryptRSA(byte[] encrypted, string privateKey)
        {
            var provider = new RSACryptoServiceProvider();
            provider.FromXmlString(privateKey);
            var decrypted = provider.Decrypt(encrypted, false);
            Dump("decrypted", decrypted);
            Console.WriteLine(Encoding.ASCII.GetString(decrypted));
        }
        #endregion

        #region AES
        private void AES()
        {
            AESDecrypt(AESEncrypt());
        }

        private void AESDecrypt((byte[] cipherText, byte[] key, byte[] initializationVector) p)
        {
            using Aes aes = Aes.Create();
            aes.Key = p.key;
            aes.IV = p.initializationVector;

            var decryptor = aes.CreateDecryptor();
            using MemoryStream memoryStream = new MemoryStream(p.cipherText);
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using (StreamReader sr = new StreamReader(cryptoStream))
            {
                Console.WriteLine(sr.ReadToEnd());
            }
        }

        private (byte[] cipherText, byte[] key, byte[] initializationVector) AESEncrypt()
        {
            string plainText = "This is my super secret data";
            byte[] cipherText;
            using Aes aes = Aes.Create();
            byte[] key = aes.Key;
            byte[] initializationVector = aes.IV;

            ICryptoTransform encryptor = aes.CreateEncryptor();
            using MemoryStream encryptMemoryStream = new MemoryStream();
            using CryptoStream encryptCryptoStream = new CryptoStream(encryptMemoryStream, encryptor, CryptoStreamMode.Write);
            using (StreamWriter swEncrypt = new StreamWriter(encryptCryptoStream))
            {
                swEncrypt.Write(plainText);
            }

            cipherText = encryptMemoryStream.ToArray();
            Dump("key", key);
            Dump("IV", initializationVector);
            Dump("cipher", cipherText);

            return (cipherText, key, initializationVector);
        }

        #endregion
    }
}
