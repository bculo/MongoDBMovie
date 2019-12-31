using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TBP.Interfaces;
using TBP.Options;

namespace TBP.Services
{
    public class PasswordHasher : IPassword
    {
        protected readonly SecurityOptions _security;

        [ActivatorUtilitiesConstructor]
        public PasswordHasher(IOptions<SecurityOptions> options)
        {
            _security = options.Value;
        }

        public PasswordHasher(SecurityOptions options)
        {
            _security = options;
        }

        public virtual async Task<bool> CheckPassword(string userCredentials, string plainPassword)
        {
            return await Task.Run(() =>
            {
                //format {salt}.{password}
                string[] passwordParts = userCredentials.Split(_security.PasswordDelimiter);

                //duljina polja mora biti 2 (imamo salt i password)
                if (passwordParts.Length != 2)
                    return false;

                //salt pretvaramo nazad u bytove kako bi mogli hashirati upisani password
                var salt = Convert.FromBase64String(passwordParts.ElementAt(0));

                //hashiraj password
                string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: plainPassword,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: _security.PasswordIterations,
                    numBytesRequested: _security.DerivedKeyLength));

                //da li je hashirani password jedank korisnickoj lozinki
                return hashedPassword == passwordParts.ElementAt(1);
            });
        }

        public virtual async Task<string> HashPassword(string plainPassword)
        {
            return await Task.Run(() =>
            {
                //pripremi salt koristeci klasu RandomNumberGenerator
                byte[] salt = new byte[_security.SaltLength];
                using (var rng = RandomNumberGenerator.Create())
                    rng.GetBytes(salt);

                //hashaj password koristeci HMACSHA256
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: plainPassword,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: _security.PasswordIterations,
                    numBytesRequested: _security.DerivedKeyLength));

                //konvertaj byte u string
                string saltString = Convert.ToBase64String(salt);

                //vrati password u formatu {salt}.{password}
                return $"{saltString}{_security.PasswordDelimiter}{hashed}";
            });
        }
    }
}
