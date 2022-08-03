using Cysharp.Threading.Tasks;

public class Server : ServerBase
{
    public override async UniTask<T> RegID<T>(string loginId, string uuid, int login_type, string name)
    {
        var data = new REGID_REQ()
        {
            login_id = loginId,
            uuid = uuid,
            login_type = login_type,
            name = name,
            
            //=========
            cmd = 101,
            aidx = 0,
            session = "",
        };
        var res = await ServerManager.Instance.Request<T>(RequestType.POST, data);
        Observable.EventAsObservable().OnNext(new Observable.EventInfo(ObservableEvent.Event.OnSC_REGID, res));
        return res;
    }
    public override async UniTask<T> Login<T>(string login_id, string uuid, short login_type, short os_type)
    {
        var data = new LOGIN_REQ
        {
            cmd = 103,
            
            login_id = "test_id",
            uuid = "test_uuid",
            login_type = 0,
        };
        var res = await ServerManager.Instance.Request<T>(RequestType.POST, data);
        Observable.EventAsObservable().OnNext(new Observable.EventInfo(ObservableEvent.Event.OnSC_LOGIN, res));
        return res;
    }
}