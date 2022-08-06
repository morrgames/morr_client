using UniRx;
using UnityEngine;
using UnityEngine.UI;

//ScriptTest 씬에서 테스트할 스크립트.
public class Test : MonoBehaviour
{
    [SerializeField] private Button btn01;
    [SerializeField] private Button btn02;

    private void Awake()
    {
        btn01.OnClickAsObservable().Subscribe(_ =>
        {
            Debug.Log("버튼01 클릭");
            //do something...
            
            
        }).AddTo(gameObject);
        btn02.OnClickAsObservable().Subscribe(_ =>
        {
            Debug.Log("버튼02 클릭");
            //do something...
            
            
            
        }).AddTo(gameObject);
    }
}
