using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UI_Title : MonoBehaviour
{
    [SerializeField] Button btnGoLobby;

    private void Awake()
    {
        btnGoLobby?.OnClickAsObservable().Subscribe(_ =>
        {
            Debug.Log("btn 클릭!");
            SceneLoader.Instance.LoadScene(Constants.SceneName.LOBBY).Forget();
        }).AddTo(gameObject);
    }
}