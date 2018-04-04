using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager
{
    public const string loadingScene = "00_Loading";
    public const string levelScene = "01_Level";
    public const string gameScene = "Game_{0:D2}_{1:D2}";

    public static void ReLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void LoadHome()
    {
        SceneManager.LoadScene(levelScene);
    }

    public static void LoadGame(int map,int level)
    {
        string sceneName = string.Format(gameScene, map, level);
        var scene = SceneManager.GetSceneByName(sceneName);
        if (scene != null)
        {
            SceneManager.LoadScene(sceneName);
        }

    }
}
