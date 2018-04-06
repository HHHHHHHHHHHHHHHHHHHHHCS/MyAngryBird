using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager
{
    public const string mapLoadingScene = "00_MapLoading";
    public const string mapLevelScene = "01_MapLevel";
    public const string gamelLoadingScene = "02_GamelLoading";
    public const string gameScene = "Game_{0:D2}_{1:D2}";

    public static void ReLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void LoadHome()
    {
        SceneManager.LoadScene(mapLoadingScene);
    }

    public static void AsyncLoadMapLevelScene()
    {
        var mAsyncOperation = SceneManager.LoadSceneAsync(mapLevelScene);
        mAsyncOperation.allowSceneActivation = true;
    }

    public static bool LoadGamelLoading(int map, int level)
    {
        string sceneName = string.Format(gameScene, map, level);
        if (level<JsonManager.Instance.ReadMaxLevelCount())
        {
            LevelLoading.SetNowMapAndLevel(sceneName);
            SceneManager.LoadScene(gamelLoadingScene);
            return true;
        }
        return false;
    }

    //public static void LoadLevelGame(int map, int level)
    //{
    //    string sceneName = string.Format(gameScene, map, level);
    //    var scene = SceneManager.GetSceneByName(sceneName);
    //    if (scene != null)
    //    {
    //        SceneManager.LoadScene(sceneName);
    //    }
    //}

    public static AsyncOperation AsyncLoadLevelGameScene(string sceneName)
    {
        //var scene = SceneManager.GetSceneByName(sceneName);
        //if (scene != null)
        //{
        var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
        return asyncOperation;
        //}
        //return null;
    }
}
