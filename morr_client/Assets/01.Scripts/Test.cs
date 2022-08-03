using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class Test : MonoBehaviour
{
    public void ToTitle()
    {
        SceneLoader.Instance.LoadScene("0_Title").Forget();
    }

    public void ToLobby()
    {
        SceneLoader.Instance.LoadScene("1_Lobby").Forget();
    }

    public void RegID()
    {
        var t = UniTask.UnityAction(async () =>
        {
            
            REGID_REQ temp = await Requester.Instance.Request<REGID_REQ>(new ReqRegID()
            {
                login_id = "kk_하하1",
                login_type = 0, //0 = 게스트
                uuid = "",
                name = "하하1",
            });
            Debug.Log(temp);
        });
        t.Invoke();
    }

    public string Get_Ulid()
    {
        Ulid temp = Ulid.NewUlid();
        return temp.ToString();
    }
    
}
