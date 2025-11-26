using System.Security.Cryptography;

namespace CodingProblemsRepl.Api.Utilities;
public class EncryptionKeys
{
    public byte[] DefaultKey => DecodeKey(DevelopmentKey);
    public string DevelopmentKey { get; set; } = "";

    private static byte[] DecodeKey(string key)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        return Convert.FromBase64String(key);
    }

    public string CreateBase64EncodedKey()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(32);
        var hash = SHA256.HashData(randomBytes);

        return Convert.ToBase64String(hash);
    }
}


