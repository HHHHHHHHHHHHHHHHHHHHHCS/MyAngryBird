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

        InitBirds();
        InitPigs();
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
        foreach(Transform item in parent)
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
        if (birdsList.Count>1)
        {
            birdsList.RemoveAt(0);
            birdsList[0].Enable(birdPos);
        }
    }

    public void RemovePig(Pig pig)
    {
        pigsList.Remove(pig);
    }
}
