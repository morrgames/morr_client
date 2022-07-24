using UnityEngine;

public partial class DataManager 
{
    private void InitData_Event()
    {
        Observable.Subscribe(ObservableEvent.Event.Test, OnTest_Temp);
        Observable.Subscribe(ObservableEvent.Event.OnSC_LOGIN, OnSC_LOGIN);
    }

    void OnTest_Temp(Observable.EventInfo _e)
    {
            
    }

    void OnSC_LOGIN(Observable.EventInfo _e)
    {
        var data = (LOGIN_RES) _e.args[0];
    }
}