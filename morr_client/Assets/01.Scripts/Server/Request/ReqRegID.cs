using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class ReqRegID : RequestBase
{
    public string login_id { get; set; }
    public int login_type { get; set; }
    public string uuid { get; set; }
    public string name { get; set; }

    public override async UniTask<RECV_REGID_DATA> Run<RECV_REGID_DATA>()
    {
        var res = await ServerManager.Instance.server.RegID(login_id, login_type, uuid, name) as RECV_REGID_DATA;
        return res;
    }
}