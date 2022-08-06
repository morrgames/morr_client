using Cysharp.Threading.Tasks;

public abstract class RequestBase
{
    public abstract UniTask<T> Run<T>() where T : class;
}