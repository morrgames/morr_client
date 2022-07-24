using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public partial class DataManager : Singleton<DataManager>
{
    public async UniTask Init()
    {
        InitData_Event();
        InitData_Player();
        await InitData_File();
    }
}