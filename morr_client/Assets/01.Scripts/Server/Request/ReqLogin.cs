using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class ReqLogin : RequestBase
{
    public string login_id;
    public string uuid;
    public short login_type;
    public short os_type;

    public override async UniTask<T> Run<T>()
    {
            
        var res = await ServerManager.Instance.server.Login<T>(login_id, uuid, login_type, os_type);
        return res;
    }
}