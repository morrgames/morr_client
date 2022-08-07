using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UI_Lobby : MonoBehaviour
{
    [SerializeField] Button btnGoTitle;

    private void Awake()
    {
        btnGoTitle?.OnClickAsObservable().Subscribe(_ =>
        {
            Debug.Log("btn 클릭!");
            SceneLoader.Instance.LoadScene(Constants.SceneName.TITLE).Forget();
        }).AddTo(gameObject);
    }
}