using System.Collections;
using System.Collections.Generic;
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
}
