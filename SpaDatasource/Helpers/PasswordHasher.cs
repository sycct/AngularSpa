using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SpaDatasource.Helpers
{
    public class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;

        public static int Iterations { set; get; } = 10000;

        public static string Hash(string password)
        {
            //create random salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            //create hash
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            //combine salt and hash
            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            return Convert.ToBase64String(hashBytes);
        }

        public static bool Verify(string password, string hashedPassword)
        {
            if(hashedPassword == null || hashedPassword.Length <= 0)
                return false;

            //get hashbytes
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            if(hashBytes.Length < SaltSize)
                return false;

            //get salt
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            //create hash with given salt
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            //get result
            for (int i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
