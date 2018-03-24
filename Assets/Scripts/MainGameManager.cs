using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    public static MainGameManager Instance
    {
        get;
        private set;
    }

    public MainUIManager mainUIManager
    {
        get;
        private set;
    }


    private List<Bird> birdsList;
    private List<Pig> pigsList;

    private Vector3 birdPos;

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
        mainUIManager = GameObject.Find("UIRoot").GetComponent<MainUIManager>().Init();
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
        mainUIManager.ShowFailPanel();
    }

    private void SucceedGame(int count = 0)
    {
        mainUIManager.ShowSucceedPanel(Mathf.Clamp(count, 1, 3));
    }

    public void RemovePig(Pig pig)
    {
        pigsList.Remove(pig);
    }
}
