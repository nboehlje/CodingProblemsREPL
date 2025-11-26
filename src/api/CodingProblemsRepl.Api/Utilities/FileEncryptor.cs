using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace CodingProblemsRepl.Api.Utilities;

public class FileEncryptor(
    IOptions<EncryptionKeys> keys,
    ILogger<FileEncryptor> logger)
{
    private readonly IOptions<EncryptionKeys> encryptionKeys = keys;
    private readonly ILogger<FileEncryptor> _logger = logger;
    public bool TrySaveEncryptedFile(string filePath, string contents)
    {
        try
        {
            using FileStream fileStream = new(filePath, FileMode.OpenOrCreate);
            using Aes aes = Aes.Create();
            aes.Key = encryptionKeys.Value.DefaultKey;

            byte[] initVector = aes.IV;
            fileStream.Write(initVector, 0, initVector.Length);

            using CryptoStream cryptoStream = new(
                fileStream,
                aes.CreateEncryptor(),
                CryptoStreamMode.Write);

            // By default, the StreamWriter uses UTF-8 encoding.
            // To change the text encoding, pass the desired encoding as the second parameter.
            // For example, new StreamWriter(cryptoStream, Encoding.Unicode).
            using StreamWriter encryptWriter = new(cryptoStream);
            encryptWriter.Write(contents);
            encryptWriter.WriteLine(contents);

            _logger.LogInformation("The file was encrypted.");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("The encryption failed. {ex}", ex);
            return false;
        }
    }

    public bool TryDecryptFile(string filePath, out string? contents)
    {
        try
        {
            using FileStream fileStream = new(filePath, FileMode.Open);
            using Aes aes = Aes.Create();
            byte[] key = encryptionKeys.Value.DefaultKey;
            byte[] initVector = new byte[aes.IV.Length];

            fileStream.ReadExactly(initVector);

            using CryptoStream cryptoStream = new(
                fileStream,
                aes.CreateDecryptor(key, initVector),
                CryptoStreamMode.Read);

            // By default, the StreamReader uses UTF-8 encoding.
            // To change the text encoding, pass the desired encoding as the second parameter.
            // For example, new StreamReader(cryptoStream, Encoding.Unicode).
            using StreamReader decryptReader = new(cryptoStream);
            contents = decryptReader.ReadToEnd();

            _logger.LogInformation("The decrypted original message: {decryptedMessage}", contents);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("The decryption failed. {ex}", ex);
            contents = null;
            return false;
        }
    }
}