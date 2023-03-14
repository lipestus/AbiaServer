using Konscious.Security.Cryptography;
using System;
using System.Collections;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SQLConnector.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public byte[] HashedPassword { get; set; }

        public UserModel(string accountName, string password)
        {
            AccountName = accountName;
            HashedPassword = HashPassword(password);
        }

        private byte[] HashPassword(string password)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                DegreeOfParallelism = 1,
                MemorySize = 1024 * 1024,
                Iterations = 2,
                Salt = GenerateSalt()
            };
            return argon2.GetBytes(16);
        }

        private byte[] GenerateSalt()
        {
            var salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        public bool VerifyPassword(string password)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = HashedPassword.Take(16).ToArray()
            };
            var hashedBytes = argon2.GetBytes(16);
            return StructuralComparisons.StructuralEqualityComparer.Equals(hashedBytes, HashedPassword);
        }
    }
}
