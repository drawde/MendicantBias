using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MendicantBias.Util
{
    public class SecretString
    {
        public static string EncryptionString(string str)
        {
            str = Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            string result = BitConverter.ToString(md5.ComputeHash(bytes));
            return result.Replace("-", "");
        }
    }
}
