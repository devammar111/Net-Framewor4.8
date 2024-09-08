using System;
using System.Security.Cryptography;
using System.Text;

namespace PBASE.Entity.Helper
{
    public static class CryptoEngine
    {
        public static string key = "dSgVkXp2s5v8y/B?"; //128-164 bit
        public static string Encrypt(string input)
        {
            byte[] inputArray = Encoding.UTF8.GetBytes(input);
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return ConvertStringToHex(Convert.ToBase64String(resultArray, 0, resultArray.Length), Encoding.Unicode);
        }

        public static string ConvertStringToHex(string input, Encoding encoding)
        {
            Byte[] stringBytes = encoding.GetBytes(input);
            StringBuilder sbBytes = new StringBuilder(stringBytes.Length * 2);
            foreach (byte b in stringBytes)
            {
                sbBytes.AppendFormat("{0:X2}", b);
            }
            return sbBytes.ToString();
        }

        public static string Decrypt(string input)
        {
            byte[] inputArray = Convert.FromBase64String(ConvertHexToString(input, Encoding.Unicode));
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
            tripleDES.Key = Encoding.UTF8.GetBytes(key);
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            tripleDES.Clear();
            return Encoding.UTF8.GetString(resultArray);
        }

        public static string ConvertHexToString(string hexInput, Encoding encoding)
        {
            int numberChars = hexInput.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexInput.Substring(i, 2), 16);
            }
            return encoding.GetString(bytes);
        }

        public static int GetId(string input)
        {
            int id = int.MinValue;
            try
            {
                int.TryParse(Decrypt(input), out id);
                return id;
            }
            catch (Exception)
            {
                //Handle Invalid Data
            }
            return id;
        }
    }
}