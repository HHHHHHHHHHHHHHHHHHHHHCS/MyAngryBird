using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoading : MonoBehaviour
{
    private static string sceneName;

    private Text text;
    private float maxLoadLength = 0.9f;
    private AsyncOperation async;

    public static void SetNowMapAndLevel(string _scenenName)
    {
        sceneName = _scenenName;
    }

    private void Start()
    {
        text = GameObject.Find("UIRoot").GetComponentInChildren<Text>();
        StartCoroutine(WaitAsync());
    }

    private IEnumerator WaitAsync()
    {
        async = GameSceneManager.AsyncLoadLevelGameScene(sceneName);
        yield return async;
    }

    private void Update()
    {
        if(async!=null)
        {
            if(async.progress< maxLoadLength)
            {
                text.text = string.Format("{0:F2}%", async.progress / maxLoadLength) ;
            }
            async.allowSceneActivation = true;
        }
    }
}
