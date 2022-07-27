using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    const string PATH = "UI/SceneLoader";

    [SerializeField] Image imgLoadingBar = null;
    [SerializeField] Text textLoadingProgress = null;

    #region singleton

    private static SceneLoader _instance = null;

    public static SceneLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SceneLoader>();


                if (_instance == null)
                {
                    var _sceneLoaderPrefab = Resources.Load<SceneLoader>(PATH);
                    _instance = Instantiate(_sceneLoaderPrefab);
                }
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = GetComponent<SceneLoader>();
            transform.SetParent(null);
            DontDestroyOnLoad(this);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(this);
            }
        }
    }

    #endregion

    public async UniTaskVoid LoadScene(string _sceneName)
    {
        SetProgress_Text(0);
        SetProgressBar(0);

        gameObject.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));// test
        await SceneManager.LoadSceneAsync(_sceneName).ToUniTask(Progress.Create<float>(x =>
        {
            SetProgress_Text(x);
            SetProgressBar(x);
        }));
        await UniTask.Delay(TimeSpan.FromSeconds(1f));// test
        gameObject.SetActive(false);
    }


    void SetProgress_Text(float _progress)
    {
        textLoadingProgress.text = $"Loading...{_progress * 100}%";
    }

    void SetProgressBar(float _progress)
    {
        imgLoadingBar.fillAmount = _progress;
    }
}