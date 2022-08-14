using System.Globalization;
using TMPro;
using UnityEngine.UI;

public static class UITools
{
    public static void SetLabelText(TMP_Text text, string str)
    {
        if (text == null) return;
        text.text = string.IsNullOrEmpty(str) ? "" : str;
    }
}