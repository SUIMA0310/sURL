using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using HashidsNet;

namespace sURL.Services
{
    public interface IHashidsService
    {
        // GetHashids() の糖衣構文
        IHashids Hashids => GetHashids();

        IHashids GetHashids();
    }

    public class HashidsService : IHashidsService
    {
        private Hashids _Hashids = null;
        private object _LockObj = new object();

        public virtual IHashids GetHashids()
        {
            lock(_LockObj)
            {
                return _Hashids ?? (_Hashids = CreateHashids());
            }
        }

        protected virtual Hashids CreateHashids()
        {
            string salt = GetSalt(Constants.SaltFileName);

            return new Hashids(salt);
        }

        protected virtual string GetSalt(string fileName)
        {
            if(fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if(File.Exists(fileName))
            {
                // Get SaltData from Strage.
                return ReadSalt(fileName);
            }
            else
            {
                // File not found. Generate SaltData.
                var salt = CreateSalt();

                // Saveing generated SaltData.
                WriteSalt(fileName, salt);

                return salt;
            }
        }

        protected virtual string ReadSalt(string fileName)
        {
            if(fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            using var reader = new StreamReader(fileName);
            
            return reader.ReadToEnd();
        }

        protected virtual void WriteSalt(string fileName, string salt)
        {
            if(fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if(salt == null)
            {
                throw new ArgumentNullException(nameof(salt));
            }

            using var writer = new StreamWriter(fileName);

            writer.Write(salt);
        }

        protected virtual string CreateSalt()
        {
            using var rngCry = new RNGCryptoServiceProvider();
            var buffer = new byte[2048];

            var hashids = new Hashids(
                salt: DateTime.Now.Ticks.ToString(),
                minHashLength: 64
            );

            // Get random bytes.
            rngCry.GetBytes(buffer);

            // make random string.
            return hashids.Encode(buffer.Select(b => (int)b)); // Can not use Cast<int>().
        }
    }
}