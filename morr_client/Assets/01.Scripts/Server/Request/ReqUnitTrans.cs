using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class ReqUnitTrans : RequestBase
{
    public int item_idx { get; set; }
    public override async UniTask<RECV_UNIT_TRANS_DATA> Run<RECV_UNIT_TRANS_DATA>()
    {
        var res = await ServerManager.Instance.server.UnitTrans(item_idx) as RECV_UNIT_TRANS_DATA;
        return res;
    }
}