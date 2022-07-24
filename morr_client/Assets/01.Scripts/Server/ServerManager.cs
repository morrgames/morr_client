using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class ServerManager : Singleton<ServerManager>
{
    private readonly string LOCAL = "http://127.0.0.1:8080";
    private readonly string REAL = "http://115.85.181.9:3306";
    private string url = "";
    public ServerBase server { get; private set; }
    public void Init()
    {
        url = REAL;
        server = new Server();
    }
    
    public async UniTask<T> Request<T>( RequestType type = RequestType.GET, object  data = null)
    {
        //make request.
        var request = new UnityWebRequest(url, type.ToString());
        if (data != null)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        }
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        
        
        //send.
        try
        {
            var res = await request.SendWebRequest();
            T result = JsonConvert.DeserializeObject<T>(res.downloadHandler.text);
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