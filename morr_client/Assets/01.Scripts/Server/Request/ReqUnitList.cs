using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class ReqUnitList : RequestBase
{
    public override async UniTask<RECV_UNIT_LIST_DATA> Run<RECV_UNIT_LIST_DATA>()
    {
        var res = await ServerManager.Instance.server.UnitList() as RECV_UNIT_LIST_DATA;
        return res;
    }
}