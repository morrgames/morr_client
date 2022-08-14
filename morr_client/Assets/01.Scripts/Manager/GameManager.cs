using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public partial class GameManager : Singleton<GameManager>
{
    private bool isInit = false;
    protected override void Awake()
    {
        base.Awake();
        if (isInit) return;
        isInit = true;
        DataManager.Instance.Init();
        ServerManager.Instance.Init();
    }

}