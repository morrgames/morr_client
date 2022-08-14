public class ObservableEvent
{
    public enum Event
    {
        //events.
        OnComplete_LoadData,
        OnUpdateLanguage,
        
        //UI onClick.
        OnClick_UpdateLanguage,
        
        //server.
        OnRecv_RegId,
        OnRecv_Login,
        OnRecv_ItemList,
        OnRecv_GetExp,
        OnRecv_UnitList,
        OnRecv_UnitTrans,
    }
}