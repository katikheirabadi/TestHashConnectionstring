using System.Security.Cryptography;
using System.Text;


using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
{
    Console.WriteLine("Enter String :");
    var str = Console.ReadLine();

    byte[] encrypted = Encrypt(str, tdes.Key, tdes.IV);

    StringBuilder hashString = new StringBuilder();
    foreach (byte x in encrypted) hashString.Append(x.ToString("x2"));

    Console.WriteLine($"Encrypted data: {hashString}");


    string decrypted = Decrypt(encrypted, tdes.Key, tdes.IV);
    Console.WriteLine($"Decrypted data: {decrypted}");
}



    static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
{
    byte[] encrypted;
    using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
    {
        ICryptoTransform encryptor = tdes.CreateEncryptor(Key, IV);
        using (MemoryStream ms = new MemoryStream())
        {
            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter sw = new StreamWriter(cs))
                    sw.Write(plainText);
                encrypted = ms.ToArray();
            }
        }
    }
    return encrypted;
}

static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
{
    string plaintext = null;
    using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
    {
        ICryptoTransform decryptor = tdes.CreateDecryptor(Key, IV);
        using (MemoryStream ms = new MemoryStream(cipherText))
        {
            using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            {
                using (StreamReader reader = new StreamReader(cs))
                    plaintext = reader.ReadToEnd();
            }
        }
    }
    return plaintext;
}