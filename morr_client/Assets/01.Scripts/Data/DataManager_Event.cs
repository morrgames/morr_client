using UnityEngine;

public partial class DataManager 
{
    private void InitData_Event()
    {
        Observable.Subscribe(ObservableEvent.Event.Test, OnTest_Temp);
    }

    void OnTest_Temp(Observable.EventInfo _e)
    {
            
    }
}