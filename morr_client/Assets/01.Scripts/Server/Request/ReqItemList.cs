using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class ReqItemList : RequestBase
{
    public override async UniTask<RECV_ITEM_LIST_DATA> Run<RECV_ITEM_LIST_DATA>()
    {
        var res = await ServerManager.Instance.server.ItemList() as RECV_ITEM_LIST_DATA;
        return res;
    }
}