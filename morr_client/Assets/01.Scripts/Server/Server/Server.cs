using Cysharp.Threading.Tasks;

public class Server : ServerBase
{
    public override async UniTask<RECV_REGID_DATA> RegID(string loginId, int login_type, string uuid, string name)
    {
        var data = new SEND_REGID_DATA
        {
            login_id = null,
            uuid = null,
            login_type = 0,
            name = null,

            //=========
            cmd = (int) PacketCode.REG_ID,
            aidx = 0,
            session = "",
        };
        var res = await ServerManager.Instance.Request<RECV_REGID_DATA>(RequestType.POST, data);
        Observable.EventAsObservable().OnNext(new Observable.EventInfo(ObservableEvent.Event.OnRecv_RegId, res));
        return res;
    }

    public override async UniTask<RECV_LOGIN_DATA> Login(int os_type, string login_id, string uuid, int login_type)
    {
        var data = new SEND_LOGIN_DATA
        {
            os_type = 0,
            login_id = null,
            login_type = 0,
            uuid = null,

            //=========
            cmd = (int) PacketCode.LOGIN,
            aidx = 0,
            session = null
        };
        var res = await ServerManager.Instance.Request<RECV_LOGIN_DATA>(RequestType.POST, data);
        Observable.EventAsObservable().OnNext(new Observable.EventInfo(ObservableEvent.Event.OnRecv_Login, res));
        return res;
    }

    public override async UniTask<RECV_ITEM_LIST_DATA> ItemList()
    {
        var data = new SEND_ITEM_LIST_DATA
        {
            //=========
            cmd = (int) PacketCode.ITEM_LIST,
            aidx = 0,
            session = null
        };
        var res = await ServerManager.Instance.Request<RECV_ITEM_LIST_DATA>(RequestType.POST, data);
        Observable.EventAsObservable().OnNext(new Observable.EventInfo(ObservableEvent.Event.OnRecv_ItemList, res));
        return res;
    }

    public override async UniTask<RECV_GET_EXP_DATA> GetExp()
    {
        var data = new SEND_GET_EXP_DATA()
        {
            //=========
            cmd = (int) PacketCode.GET_EXP,
            aidx = 0,
            session = null
        };
        var res = await ServerManager.Instance.Request<RECV_GET_EXP_DATA>(RequestType.POST, data);
        Observable.EventAsObservable().OnNext(new Observable.EventInfo(ObservableEvent.Event.OnRecv_GetExp, res));
        return res;
    }

    public override async UniTask<RECV_UNIT_LIST_DATA> UnitList()
    {
        var data = new SEND_UNIT_LIST_DATA
        {
            //=========
            cmd = (int) PacketCode.UNIT_LIST,
            aidx = 0,
            session = null
        };
        var res = await ServerManager.Instance.Request<RECV_UNIT_LIST_DATA>(RequestType.POST, data);
        Observable.EventAsObservable().OnNext(new Observable.EventInfo(ObservableEvent.Event.OnRecv_UnitList, res));
        return res;
    }

    public override async UniTask<RECV_UNIT_TRANS_DATA> UnitTrans(int item_idx)
    {
        var data = new SEND_UNIT_TRANS_DATA
        {
            item_idx = 0,
            //=========
            cmd = (int) PacketCode.UNIT_TRANS,
            aidx = 0,
            session = null,
        };
        var res = await ServerManager.Instance.Request<RECV_UNIT_TRANS_DATA>(RequestType.POST, data);
        Observable.EventAsObservable().OnNext(new Observable.EventInfo(ObservableEvent.Event.OnRecv_UnitTrans, res));
        return res;
    }
}