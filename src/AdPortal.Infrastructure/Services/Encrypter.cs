using System;
using System.Security.Cryptography;
using AdPortal.Infrastructure.Extensions;

namespace AdPortal.Infrastructure.Services
{
   
    public class Encrypter : IEncrypter
    {
        private readonly int DeriveBytesIntegrationsCount = 10000;
        private readonly int SaltSize = 40;

        public string GetSalt(string value)
        {
            if(value.Empty())
            {
                throw new ArgumentNullException("Can not generate salt from empty value");
            }
            var random = new Random();
            var saltBytes = new byte[SaltSize];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        public string GetHash(string value, string salt)
        {
            if(value.Empty())
            {
                throw new ArgumentNullException("Can not generate hash from empty value");
            }
             if(salt.Empty())
            {
                throw new ArgumentNullException("Can not use empty salt to generate hash");
            }
            var pbkdf2 = new Rfc2898DeriveBytes(value,GetBytes(salt),DeriveBytesIntegrationsCount);

            return Convert.ToBase64String(pbkdf2.GetBytes(SaltSize));
        }

        public static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length*sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(),0,bytes,0,bytes.Length);

            return bytes;
        }
    }
}