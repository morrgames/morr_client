using System;
using TMPro;
using UniRx;
using UnityEngine;

public class LocalizedTMP : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    private string key = null;
    private object[] args;

    void Awake()
    {
        Observable.Subscribe(gameObject, ObservableEvent.Event.OnUpdateLanguage, OnUpdateLanguage);
    }

    public void SetText(string key, params object[] args)
    {
        this.key = key;
        this.args = args;
        Display();
    }

    void Display()
    {
        var localized = DataManager.Instance.GetLocalizedText(key);
        var value = string.Format(localized, args);
        UITools.SetLabelText(text, value);
    }

    void OnUpdateLanguage(Observable.EventInfo _e)
    {
        Display();
    }
}