using Cysharp.Threading.Tasks;

public class Requester : Singleton<Requester>
{
    public async UniTask<T> Request<T>(RequestBase requestBase)
    {
        var res = await requestBase.Run<T>();
        return res;
    }
}