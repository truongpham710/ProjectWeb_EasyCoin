using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Easybook.Api.BusinessLogic.EasyWallet.Utility
{
    public static class SecurityLogic
    {      
        
        public static string GetRandomHexNumber(int numberOfDigits)
        {
            var buffer = new byte[numberOfDigits / 2];
            var random = new Random();

            random.NextBytes(buffer);
            var result = string.Concat(buffer.Select(x => x.ToString("X2")).ToArray());

            return (numberOfDigits % 2 == 0) ? result : result + random.Next(16).ToString("X");
        }
        public static string GetSha1Hash(string text)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();

            // Compute hash from the bytes of text.
            sha1.ComputeHash(Encoding.ASCII.GetBytes(text));

            // Get hash result after computations
            var hashResults = sha1.Hash;

            return Convert.ToBase64String(hashResults);
        }
        public static string GenerateKey(int lengthKey)
        {
            return  Guid.NewGuid().ToString().Replace("-","").Substring(0, lengthKey);
        }

      

        public static int GenerateIntRandom()
        {
            Random random = new Random();
            return random.Next(10, 1000);
        }
        public class Globals
        {
            private static string _stampServerKey;
            public static string StampServerKey
            {
                get
                {      
                    return _stampServerKey;
                }
                set
                {                
                    _stampServerKey = value;
                }
            }         
        }
    }

  

}
