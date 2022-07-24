using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//팝업 프리팹에 붙을 스크립트
public class UIPopup_Sample : UIBase
{
    [SerializeField] private Text level;

    public override void Open()
    {
        base.Open();
        level.text = "Lv.999";
    }

    public override void Close()
    {
        base.Close();
    }
}