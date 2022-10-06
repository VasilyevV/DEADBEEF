using System;
using System.IO;
using System.Security.Cryptography;

namespace DEADBEEF
{
    internal static class GetHash
    {
        internal static string Hash(string path)
        {
            var md5 = MD5.Create();
            var stream = File.OpenRead(path);
            string result = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", String.Empty);
            return result;
        }
    }
}
