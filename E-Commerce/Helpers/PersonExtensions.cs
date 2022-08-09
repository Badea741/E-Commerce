using System.Security.Cryptography;
using System.Text;

namespace ECommerce;
public static class PersonExtensions
{
    public static string ToSHA256(this string password)
    {
        var sb = new StringBuilder();
        using (SHA256 algorithm = SHA256.Create())
        {
            byte[] passwordBytes = algorithm.ComputeHash(
                    Encoding.UTF8.GetBytes(password));
            foreach (byte b in passwordBytes)
            {
                sb.Append(b.ToString("X2"));
            }
        }
        return sb.ToString();

    }
}