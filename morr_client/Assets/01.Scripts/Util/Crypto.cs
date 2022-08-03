using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public class Crypto
{
    private readonly string KEYSET = "abcdefghijklmnopqrstuvwxyz@))$!!)@=ABCDEFGHIJKLMNOPQRSTUVWXYZ@))$!!)@=";

    private string MakeRandom_4Key()
    {
        return "abcd"; //test
        int len = KEYSET.Length - 1;
        var rnd = new Random();
        StringBuilder aes = new StringBuilder();
        for (var i = 0; i < 4; i++)
        {
            int rand = rnd.Next(0, len);
            aes.Append(KEYSET[rand]);
        }

        return aes.ToString();
    }

    public string Encryption(string Input)
    {
        string _4key = MakeRandom_4Key();
        string _32key = string.Concat(Enumerable.Repeat(_4key, 8));

        RijndaelManaged aes = new RijndaelManaged();
        aes.KeySize = 256;
        aes.BlockSize = 128;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = Encoding.UTF8.GetBytes(_32key);
        aes.IV = new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

        var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
        byte[] xBuff = null;
        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
            {
                byte[] xXml = Encoding.UTF8.GetBytes(Input);
                cs.Write(xXml, 0, xXml.Length);
            }

            xBuff = ms.ToArray();
        }

        string result = _4key + Convert.ToBase64String(xBuff);
        return result;
    }

    public string Decryption(string Input)
    {
        string _4key = Input.Substring(0, 4);
        string _32key = string.Concat(Enumerable.Repeat(_4key, 8));
        string data = Input.Substring(4, Input.Length - 4);
        RijndaelManaged aes = new RijndaelManaged();
        aes.KeySize = 256;
        aes.BlockSize = 128;
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = Encoding.UTF8.GetBytes(_32key);
        aes.IV = new byte[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

        var decrypt = aes.CreateDecryptor();
        byte[] xBuff = null;
        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
            {
                byte[] xXml = Convert.FromBase64String(data);
                cs.Write(xXml, 0, xXml.Length);
            }

            xBuff = ms.ToArray();
        }

        String Output = Encoding.UTF8.GetString(xBuff);
        return Output;
    }
}