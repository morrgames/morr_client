using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class ServerManager : Singleton<ServerManager>
{
    private readonly string LOCAL = "http://127.0.0.1:8080";
    private readonly string REAL = "http://49.50.161.193:8080";
    private string url;
    public ServerBase server { get; private set; }
    public void Init()
    {
        url = REAL;
        //url = LOCAL;
        server = new Server();
    }
    public async UniTask<T> Request<T>( RequestType type = RequestType.GET, object  data = null)
    {
        //make request.
        var request = new UnityWebRequest(url, type.ToString());
        if (data != null)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            byte[] jsonByteData = Encoding.UTF8.GetBytes(jsonData);
            byte[] compressedData = Compress.CompressData(jsonByteData);
            string encryption = Crypto.Encryption(compressedData);
            byte[] encryptionByteData = Encoding.UTF8.GetBytes(encryption);
            request.uploadHandler = new UploadHandlerRaw(encryptionByteData);
        }
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        
        //send & receive
        var res = await request.SendWebRequest();
        {
            byte[] downloadByteData = res.downloadHandler.data;
            string downloadData = Encoding.UTF8.GetString(downloadByteData);
            byte[] decryption = Crypto.Decryption(downloadData);
            byte[] DecompressdData = Compress.DecompressData(decryption);
            string jsonData =Encoding.UTF8.GetString(DecompressdData);
            T result = JsonConvert.DeserializeObject<T>(jsonData);
            return result;
        }
    }
}