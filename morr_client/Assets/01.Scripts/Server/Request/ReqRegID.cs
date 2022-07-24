using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class ReqRegID : RequestBase
{
    public string login_id;
    public string uuid;
    public int login_type;
    public string name;

    public override async UniTask<T> Run<T>()
    {
        var res = await ServerManager.Instance.server.RegID<T>(login_id, uuid, login_type, name);
        return res;
    }
}