namespace LazyEvo.Plugins
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    internal class Encryptor
    {
        internal static List<string> Text = CreateKeys();

        private static List<string> CreateKeys() => 
            new List<string> { 
                "lazy_evo",
                "hsf_klfa",
                "ios_dnsd",
                "asi_yqnx",
                "pig_sdkl",
                "dgi_ckja",
                "soa_adnc",
                "wod_cmcs",
                "kds_dsns",
                "vnq_isxm",
                "evolution",
                "xalipxasm",
                "feipofwls",
                "asdmvnkds",
                "ewmchysow",
                "czxjcnlck",
                "oqwieqocc",
                "auxcnnmxs",
                "skcguhsvz",
                "zzjcsumdh",
                "@1B2c3D4e5F6g7H8",
                "!sf2SDl9j2@d8GdC",
                "D@kd9KMl87asdxE3",
                "d&@D9lg5n6F1!iB8",
                "18wegaSd@saf!fjf",
                "9fENs7D!cfnCsdu2",
                "casD7Sn@!nc@mc2S",
                "Snc@mc!B26dFm1N1",
                "cCbjsY2@Ksb72D68",
                "c2!mNdO8D52d97Gc"
            };

        internal static string Decrypt(string input)
        {
            string str;
            try
            {
                int num = int.Parse(input.Substring(input.Length - 2, 1));
                int num2 = int.Parse(input.Substring(0, 1));
                int num3 = int.Parse(input.Substring(input.Length - 1, 1));
                str = Decrypt(input.Substring(1, input.Length - 3), Text[num], Text[num2 + 10], "SHA1", 2, Text[num3 + 20], 0x100);
            }
            catch (Exception)
            {
                throw new Exception("Could not decrypt relogging information");
            }
            return str;
        }

        internal static string Decrypt(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] buffer = Convert.FromBase64String(cipherText);
            RijndaelManaged managed = new RijndaelManaged {
                Mode = CipherMode.CBC
            };
            MemoryStream stream = new MemoryStream(buffer);
            CryptoStream stream2 = new CryptoStream(stream, managed.CreateDecryptor(new PasswordDeriveBytes(passPhrase, bytes, hashAlgorithm, passwordIterations).GetBytes(keySize / 8), Encoding.ASCII.GetBytes(initVector)), CryptoStreamMode.Read);
            byte[] buffer5 = new byte[buffer.Length];
            stream.Close();
            stream2.Close();
            return Encoding.UTF8.GetString(buffer5, 0, stream2.Read(buffer5, 0, buffer5.Length));
        }

        internal static string Encrypt(string input)
        {
            Random random = new Random();
            int num = random.Next(9);
            int num2 = random.Next(9);
            int num3 = random.Next(9);
            object[] objArray = new object[] { num2, Encrypt(input, Text[num], Text[num2 + 10], "SHA1", 2, Text[num3 + 20], 0x100), num, num3 };
            return string.Concat(objArray);
        }

        internal static string Encrypt(string plainText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(saltValue);
            byte[] buffer = Encoding.UTF8.GetBytes(plainText);
            RijndaelManaged managed = new RijndaelManaged {
                Mode = CipherMode.CBC
            };
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, managed.CreateEncryptor(new PasswordDeriveBytes(passPhrase, bytes, hashAlgorithm, passwordIterations).GetBytes(keySize / 8), Encoding.ASCII.GetBytes(initVector)), CryptoStreamMode.Write);
            stream2.Write(buffer, 0, buffer.Length);
            stream2.FlushFinalBlock();
            byte[] inArray = stream.ToArray();
            stream.Close();
            stream2.Close();
            return Convert.ToBase64String(inArray);
        }
    }
}

