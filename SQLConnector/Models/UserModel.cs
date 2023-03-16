using Konscious.Security.Cryptography;
using System;
using System.Collections;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SQLConnector.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public byte[] HashedPassword { get; set; }
        public bool IsAccountNameValid { get; set; }
        public byte[] Salt { get; set; }
        public UserModel() { }
        public UserModel(string accountName, string password)
        {
            if (Regex.IsMatch(accountName, @"^(?!.*\b(asshole|fuck|shit|cunt|whore|nigger|retard)).+$", RegexOptions.IgnoreCase))
            {
                AccountName = accountName;
                IsAccountNameValid = true;
            }
            else
            {
                IsAccountNameValid = false;
                AccountName = null;
            }

            Salt = GenerateSalt();
            HashedPassword = HashPassword(password, Salt);
        }

        private byte[] HashPassword(string password, byte[] salt)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                DegreeOfParallelism = 1,
                MemorySize = 32768,
                Iterations = 2,
                Salt = salt
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
                DegreeOfParallelism = 1,
                MemorySize = 32768,
                Iterations = 2,
                Salt = Salt
            };
            var hashedBytes = argon2.GetBytes(16);
            return StructuralComparisons.StructuralEqualityComparer.Equals(hashedBytes, HashedPassword);
        }
    }
}
