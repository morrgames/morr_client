using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class ReqLogin : RequestBase
{
    public int os_type { get; set; }
    public string login_id { get; set; }
    public string uuid { get; set; }
    public int login_type { get; set; }


    public override async UniTask<RES_LOGIN_DATA> Run<RES_LOGIN_DATA>()
    {
        var res = await ServerManager.Instance.server.Login(os_type, login_id, uuid, login_type) as RES_LOGIN_DATA;
        return res;
    }
}