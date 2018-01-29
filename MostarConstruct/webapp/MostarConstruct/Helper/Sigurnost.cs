using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MostarConstruct.Web.Helper
{
    public static class Sigurnost
    {
        private const string dopusteniObicniKarakteri = "abcdefghijklmnopqrstuvwxyz0123456789";
        private const string dopusteniSpecijalniKarakteri = "abcdefghijklmnopqrstuvwxyz0123456789!#$%&/()=?";
        public static string PostaviPocetnuLozinku() => "MostarConstruct";
        public static string GenerisiPassword(int duzina = 10, bool obicniKarakteri = true)
        {
            var bytes = new byte[duzina];

            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(bytes);
            }

            if(obicniKarakteri)
                return new string(bytes.Select(x => dopusteniObicniKarakteri[x % dopusteniObicniKarakteri.Length]).ToArray());
            return new string(bytes.Select(x => dopusteniSpecijalniKarakteri[x % dopusteniSpecijalniKarakteri.Length]).ToArray());
        }

        public static string GenerisiHash(string input)
        {
            var salt = GenerisiSalt(16);

            var bytes = KeyDerivation.Pbkdf2(input, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);

            return $"{Convert.ToBase64String(salt) }:{ Convert.ToBase64String(bytes) }";
        }

        public static bool DaLiSePodudaraju(string hash, string input)
        {
            try
            {
                var parts = hash.Split(':');

                var salt = Convert.FromBase64String(parts[0]);

                var bytes = KeyDerivation.Pbkdf2(input, salt, KeyDerivationPrf.HMACSHA512, 10000, 16);

                return parts[1].Equals(Convert.ToBase64String(bytes));
            }
            catch
            {
                return false;
            }
        }

        private static byte[] GenerisiSalt(int duzina)
        {
            var salt = new byte[duzina];

            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(salt);
            }

            return salt;
        }
    }
}
