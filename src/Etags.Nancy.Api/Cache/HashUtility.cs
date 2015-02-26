using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Etags.Nancy.Api.Cache
{
    public static class HashUtility
    {
        public static string Hash(object toHash)
        {
            var hasher = MD5.Create();
            var hashed = hasher.ComputeHash(GetBytes(toHash.ToString()));
            return GetString(hashed);
        }

        private static byte[] GetBytes(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            return bytes;
        }

        private static string GetString(byte[] bytes)
        {
            var str = Encoding.UTF8.GetString(bytes);
            return str;
        }
    }
}
