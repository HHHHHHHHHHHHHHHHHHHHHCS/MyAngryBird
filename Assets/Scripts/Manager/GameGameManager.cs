using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGameManager : MonoBehaviour
{
    public static GameGameManager Instance
    {
        get;
        private set;
    }

    public GameUIManager GameUIManager
    {
        get;
        private set;
    }

    public GameAudioManager GameAudioManager
    {
        get;
        private set;
    }

    private static int nowMap = -1, nowLevel = -1;

    private List<Bird> birdsList;
    private List<Pig> pigsList;


    private Vector3 birdPos;

    public static void SetNowMapAndLevel(int _nowMap, int _nowLevel)
    {
        nowMap = _nowMap;
        nowLevel = _nowLevel;
    }

    private void Awake()
    {
        Instance = this;
        Init();
    }

    private void Init()
    {

        InitCompents();
        InitBirds();
        InitPigs();
    }

    private void InitCompents()
    {
        GameUIManager = GameObject.Find("UIRoot").GetComponent<GameUIManager>().Init();
        GameAudioManager = GameObject.Find("GameAudioManager").GetComponent<GameAudioManager>().Init();
    }


    private void InitBirds()
    {
        birdsList = new List<Bird>();
        Transform parent = GameObject.Find("Birds").transform;
        birdPos = parent.Find("BirdPos").position;
        foreach (Transform item in parent)
        {
            var bird = item.GetComponent<Bird>();
            if (bird)
            {
                birdsList.Add(bird);
            }
        }
        if (birdsList.Count > 0)
        {
            birdsList[0].Enable(birdPos);
        }
    }

    private void InitPigs()
    {
        pigsList = new List<Pig>();
        Transform parent = GameObject.Find("Pigs").transform;
        foreach (Transform item in parent)
        {
            var pig = item.GetComponent<Pig>();
            if (pig)
            {
                pigsList.Add(pig);
            }
        }
    }

    public void MoveNextBird()
    {
        birdsList.RemoveAt(0);
        if (birdsList.Count > 0)
        {

            birdsList[0].Enable(birdPos);
        }
        CheckGameState();
    }

    private void CheckGameState()
    {
        if (birdsList.Count <= 0 && pigsList.Count > 0)
        {
            FailGame();
        }
        else if (birdsList.Count >= 0 && pigsList.Count <= 0)
        {
            SucceedGame(birdsList.Count);
        }
    }

    private void FailGame()
    {
        GameUIManager.ShowFailPanel();
    }

    private void SucceedGame(int count)
    {
        count = Mathf.Clamp(count, 1, 3);
        if (nowMap >= 0 && nowLevel >= 0)
        {
            JsonManager.Instance.UpdateLevelStar(nowMap, nowLevel, count);
        }

        GameUIManager.ShowSucceedPanel(count);
    }

    public void RemovePig(Pig pig)
    {
        pigsList.Remove(pig);
    }


    public void LoadNextLevel()
    {
    
        if(!GameSceneManager.LoadGamelLoading(nowMap, nowLevel + 1))
        {
            GameSceneManager.LoadHome();
        }
    }
}
