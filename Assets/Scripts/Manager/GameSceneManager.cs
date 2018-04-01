using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager
{
    public const string loadingScene = "00_Loading";
    public const string levelScene = "01_Level";
    public const string gameScene = "02_Game";

    public static void ReLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void LoadHome()
    {
        SceneManager.LoadScene(levelScene);
    }
}
