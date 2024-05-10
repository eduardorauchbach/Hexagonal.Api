using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexagonal.Services.Crypt
{
    public class HashService : IHashService
    {
        public string HashValue(string value)
        {
            return BCrypt.Net.BCrypt.HashPassword(value);
        }

        public bool VerifyValue(string enteredValue, string storedHash)
        {
            return BCrypt.Net.BCrypt.Verify(enteredValue, storedHash);
        }
    }
}
