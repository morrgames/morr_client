using Cysharp.Threading.Tasks;

public abstract class ServerBase
{
    public abstract UniTask<RECV_REGID_DATA> RegID(string login_id, int login_type, string uuid, string name);
    public abstract UniTask<RECV_LOGIN_DATA> Login(int os_type, string login_id, string uuid, int login_type);
    public abstract UniTask<RECV_ITEM_LIST_DATA> ItemList();
    public abstract UniTask<RECV_GET_EXP_DATA> GetExp();
    public abstract UniTask<RECV_UNIT_LIST_DATA> UnitList();
    public abstract UniTask<RECV_UNIT_TRANS_DATA> UnitTrans(int item_idx);
    
}