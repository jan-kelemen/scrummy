using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Scrummy.Domain.Core.Utilities
{
    public class SecurityUtility
    {
        public static string HashPassword(string password)
        {
            using (var hasher = SHA256.Create())
            {
                var bytes = hasher.ComputeHash(Encoding.Unicode.GetBytes(password.ToCharArray()));
                return bytes.Aggregate("", (current, b) => current + b.ToString("x2"));
            }
        }
    }
}
