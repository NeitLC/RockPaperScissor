using System.Security.Cryptography;
using System.Text;

namespace RockPaperScissor;

public class Generator
{
    public string GenerateRandomKey()
    {
        var rand = RandomNumberGenerator.Create();
        var byteArray = new byte[32];
        rand.GetBytes(byteArray);
        return BitConverter.ToString(byteArray).Replace("-", "");
    }
    
    public string GenerateHmac(string key, string message)
    {
        var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
        return BitConverter.ToString(hash).Replace("-", "");
    }
}