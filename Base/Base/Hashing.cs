using System.Security.Cryptography;
using System.Text;

namespace wK_Manager.Base {
    public static class Hashing {
        public static string MD5_Simple(string input)
            => Convert.ToHexString(MD5.HashData(Encoding.UTF8.GetBytes(input)));

        public static string SHA1_Simple(string input)
            => Convert.ToHexString(SHA1.HashData(Encoding.UTF8.GetBytes(input)));

        public static string SHA256_Simple(string input)
            => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(input)));

        public static string SHA512_Simple(string input)
            => Convert.ToHexString(SHA512.HashData(Encoding.UTF8.GetBytes(input)));
    }
}
