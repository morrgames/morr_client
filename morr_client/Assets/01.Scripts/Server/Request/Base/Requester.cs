using Cysharp.Threading.Tasks;

public class Requester : Singleton<Requester>
{
    public async UniTask<T> Request<T>(RequestBase requestBase) where T : class
    {
        var res = await requestBase.Run<T>();
        return res;
    }
}