using UnityEngine;

public partial class DataManager
{
    private void InitData_ServerEvent()
    {
        Observable.Subscribe(gameObject, ObservableEvent.Event.OnRecv_RegId, OnRecv_RegId);
        Observable.Subscribe(gameObject, ObservableEvent.Event.OnRecv_Login, OnRecv_Login);
        Observable.Subscribe(gameObject, ObservableEvent.Event.OnRecv_ItemList, OnRecv_ItemList);
        Observable.Subscribe(gameObject, ObservableEvent.Event.OnRecv_GetExp, OnRecv_GetExp);
        Observable.Subscribe(gameObject, ObservableEvent.Event.OnRecv_UnitList, OnRecv_UnitList);
        Observable.Subscribe(gameObject, ObservableEvent.Event.OnRecv_UnitTrans, OnRecv_UnitTrans);
    }

    void OnRecv_RegId(Observable.EventInfo _e)
    {
        var data = (RECV_REGID_DATA) _e.args[0];
    }

    void OnRecv_Login(Observable.EventInfo _e)
    {
        var data = (RECV_LOGIN_DATA) _e.args[0];
    }

    void OnRecv_ItemList(Observable.EventInfo _e)
    {
        var data = (RECV_ITEM_LIST_DATA) _e.args[0];
    }

    void OnRecv_GetExp(Observable.EventInfo _e)
    {
        var data = (RECV_GET_EXP_DATA) _e.args[0];
    }

    void OnRecv_UnitList(Observable.EventInfo _e)
    {
        var data = (RECV_UNIT_LIST_DATA) _e.args[0];
    }

    void OnRecv_UnitTrans(Observable.EventInfo _e)
    {
        var data = (RECV_UNIT_TRANS_DATA) _e.args[0];
    }
}