using UnityEngine;

public partial class DataManager 
{
    //todo
    private PlayerData _playerData = new PlayerData();
    private void InitData_Player()
    {
        _playerData.Init();
    }
    
    public class PlayerData
    {
        public int uid = 0;
        public int exp = 0;
        public string nickName = "";

        public void Init()
        {
            //todo.
        }
    }
}