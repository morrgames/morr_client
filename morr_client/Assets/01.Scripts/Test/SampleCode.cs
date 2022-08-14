using Cysharp.Threading.Tasks;
using UnityEngine;

//테스트가 완료된 샘플코드.
public class SampleCode
{
    public void T_UniTask()
    {
        //반환 받아야하는경우.
        var result = UniTask.Create(async () =>
        {
            await UniTask.DelayFrame(1);
            return "test";
        });

        //반환형이 void 인경우.
        UniTask.Void(async () => { await UniTask.DelayFrame(1); });
    }


    public async UniTaskVoid T_Request()
    {
        //request reqLogin
        RECV_REGID_DATA temp = await Requester.Instance.Request<RECV_REGID_DATA>(new ReqRegID()
        {
            login_id="test_001",
            login_type=0,
            uuid="test_001",
            name="~~~",
        });
        Debug.Log(temp);//do something
    }

    public void T_SceneMove()
    {
        //to Title scene.
        string sceneName = "0_Title";
        SceneLoader.Instance.LoadScene(sceneName).Forget();
    }

    public void T_LocalizedText()
    {
        //로컬라이징.
        LocalizedTMP temp = null;
        temp.SetText("key");//기본형.
        temp.SetText("key", 100, 200);//args가 있는경우.
    }
}