using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

class Sample
{
    public async UniTaskVoid Test()
    {
        LOGIN_RES temp = await Requester.Instance.Request<LOGIN_RES>(new ReqLogin()
        {
            login_id = "",
            login_type = 0,
            os_type = 1,
            uuid = "",
        });
    }
}