    using Cysharp.Threading.Tasks;

    public abstract class ServerBase
    {
        public abstract UniTask<T> RegID<T>(string loginId, string uuid, int login_type, string name);
        public abstract UniTask<T> Login<T>(string login_id, string uuid, short login_type, short os_type);
    }
