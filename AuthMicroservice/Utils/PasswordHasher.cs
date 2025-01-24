﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace AuthMicroservice.Utils
{
    public class PasswordHasher
    {


        private const int SaltSize = 16; // Tamaño del salt en bytes
        private const int KeySize = 32; // Tamaño del hash en bytes
        private const int Iterations = 10000; // Número de iteraciones

        public static string HashPassword(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(password, SaltSize, Iterations, HashAlgorithmName.SHA256))
            {
                var salt = algorithm.Salt;
                var key = algorithm.GetBytes(KeySize);

                var hash = new byte[SaltSize + KeySize];
                Array.Copy(salt, 0, hash, 0, SaltSize);
                Array.Copy(key, 0, hash, SaltSize, KeySize);

                return Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPassword(string hashedPassword, string providedPassword)
        {
            var hashBytes = Convert.FromBase64String(hashedPassword);

            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            using (var algorithm = new Rfc2898DeriveBytes(providedPassword, salt, Iterations, HashAlgorithmName.SHA256))
            {
                var key = algorithm.GetBytes(KeySize);

                for (int i = 0; i < KeySize; i++)
                {
                    if (hashBytes[SaltSize + i] != key[i])
                        return false;
                }
            }

            return true;
        }
    }
}