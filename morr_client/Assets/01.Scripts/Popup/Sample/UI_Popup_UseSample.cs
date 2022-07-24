using UnityEngine;

//팝업을 호출하는 부분.
public class UI_Popup_UseSample : MonoBehaviour
{
    private void Awake()
    {
        PopupManager.Instance.ShowPopupUI("UIPopup_Sample");
    }
}