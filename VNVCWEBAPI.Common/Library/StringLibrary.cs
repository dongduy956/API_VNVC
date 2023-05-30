using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace VNVCWEBAPI.Common.Library
{
    public static class StringLibrary
    {
        public static string PasswordHash(this string value)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(value));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public static string GenerateCode(bool isPassword = false)
        {
            Random random = new Random();
            string ZeroNumber = "";
            int codeNumber = random.Next(0, 99999);
            if (isPassword)
                codeNumber = random.Next(100000, 999999);
            int length = !isPassword ? 5 : 6;

            for (int i = 0; i < length- codeNumber.ToString().Length; i++)
            {
                ZeroNumber += "0";
            }
            return ZeroNumber + codeNumber;
        }
    }
}
