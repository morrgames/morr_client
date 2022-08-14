using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public partial class DataManager : Singleton<DataManager>
{
    public async UniTask Init()
    {
        await InitData_File();
        InitData_PlayerPrefs();
        InitData_ServerEvent();
        InitData_ClientEvent();
        InitData_Player();
    }
}