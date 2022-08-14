using UnityEngine;

public partial class DataManager
{
    private void InitData_ClientEvent()
    {
        Observable.Subscribe(gameObject, ObservableEvent.Event.OnClick_UpdateLanguage, OnClick_UpdateLanguage);
    }

    void OnClick_UpdateLanguage(Observable.EventInfo _e)
    {
        SystemLanguage lang = (SystemLanguage) _e.args[0];
        PlayerPrefs.SetInt(Constants.PlayerPrefsKey.LANGUAGE, GetLanguageCode(lang));
        
        Observable.EventAsObservable().OnNext(new Observable.EventInfo(ObservableEvent.Event.OnUpdateLanguage));
    }
}