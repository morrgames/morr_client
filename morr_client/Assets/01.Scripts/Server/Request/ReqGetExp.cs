using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class ReqGetExp : RequestBase
{
    public override async UniTask<RECV_GET_EXP_DATA> Run<RECV_GET_EXP_DATA>()
    {
        var res = await ServerManager.Instance.server.GetExp() as RECV_GET_EXP_DATA;
        return res;
    }
}