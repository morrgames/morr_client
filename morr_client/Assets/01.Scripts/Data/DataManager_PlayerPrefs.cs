    using UnityEngine;

    public partial class DataManager
    {
        void InitData_PlayerPrefs()
        {
            PlayerPrefs.SetInt(Constants.PlayerPrefsKey.LANGUAGE, GetCurrentLanguageCode());
        }
    }
