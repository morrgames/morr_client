using System.Collections.Generic;
using UnityEngine;

public partial class DataManager
{
    public string GetLocalizedText(string key)
    {
        return dicLanguage[(SystemLanguage) GetCurrentLanguageCode()][key];
    }
    private int GetCurrentLanguageCode()
    {
        return PlayerPrefs.GetInt(Constants.PlayerPrefsKey.LANGUAGE, (int) SystemLanguage.Korean);
    }

    private int GetLanguageCode(SystemLanguage lang)
    {
        int code = lang switch
        {
            SystemLanguage.Korean => (int) SystemLanguage.Korean,
            _ => (int) SystemLanguage.English,
        };
        return code;
    }

    
}

