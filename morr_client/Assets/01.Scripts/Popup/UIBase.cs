using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UIBase : MonoBehaviour
{
    public virtual void Open()
    {
        PopupManager.Instance.ShowPopupUI(this.name);
    }

    public virtual void Close()
    {
        PopupManager.Instance.ClosePopupUI(this.name);
    }

}