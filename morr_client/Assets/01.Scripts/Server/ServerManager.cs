using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class ServerManager : Singleton<ServerManager>
{
    private readonly string LOCAL = "http://127.0.0.1:8080";
    private readonly string REAL = "http://49.50.161.193:8080";
    private string url = "";
    private Crypto crypto { get; set; }
    public ServerBase server { get; private set; }
    public void Init()
    {
        url = REAL;
        //url = LOCAL;
        server = new Server();
        crypto = new Crypto();
    }
    
    public async UniTask<T> Request<T>( RequestType type = RequestType.GET, object  data = null)
    {
        //make request.
        var request = new UnityWebRequest(url, type.ToString());
        if (data != null)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            string encryption = crypto.Encryption(jsonData);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(encryption);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        }
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        
        
        //send & receive
        try
        {
            var res = await request.SendWebRequest();
            string decryption = crypto.Decryption(res.downloadHandler.text);
            T result = JsonConvert.DeserializeObject<T>(decryption);
            return result;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return default;
        }
        return default;
    }
}