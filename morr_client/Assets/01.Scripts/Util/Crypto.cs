using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public static class Crypto
{
    private static readonly string KEYSET = "abcdefghijklmnopqrstuvwxyz@))$!!)@=ABCDEFGHIJKLMNOPQRSTUVWXYZ@))$!!)@=";
    private static (string _4key, string _32key) CreateKey()
    {
        int rndIndex = new Random().Next(0, KEYSET.Length);
        string _4key = string.Concat(Enumerable.Repeat(KEYSET[rndIndex], 4));
        string _32key = string.Concat(Enumerable.Repeat(_4key, 8));
        return (_4key, _32key);
    }
    public static string Encryption(byte[] Input)
    {
        var key = CreateKey();
        RijndaelManaged aes = new RijndaelManaged
        {
            KeySize = 256,
            BlockSize = 128,
            Mode = CipherMode.CBC,
            Padding = PaddingMode.PKCS7,
            Key = Encoding.UTF8.GetBytes(key._32key),
            IV = new byte[16]
        };

        var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
        byte[] xBuff;
        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
            {
                cs.Write(Input, 0, Input.Length);
            }

            xBuff = ms.ToArray();
        }
        string result = key._4key + Convert.ToBase64String(xBuff);
        return result;
    }

    public static byte[] Decryption(string Input)
    {
        string _4key = Input.Substring(0, 4);
        string _32key = string.Concat(Enumerable.Repeat(_4key, 8));
        string data = Input.Substring(4, Input.Length - 4);

        var aes = new RijndaelManaged
        {
            KeySize = 256,
            BlockSize = 128,
            Mode = CipherMode.CBC,
            Padding = PaddingMode.PKCS7,
            Key = Encoding.UTF8.GetBytes(_32key),
            IV = new byte[16]
        };

        var decrypt = aes.CreateDecryptor();
        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
        {
            byte[] b = Convert.FromBase64String(data);
            cs.Write(b, 0, b.Length);
        }

        return ms.ToArray();
    }
}