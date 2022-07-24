using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public partial class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        DataManager.Instance.Init();
        ServerManager.Instance.Init();
    }

}