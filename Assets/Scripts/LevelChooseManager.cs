using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelChooseManager : MonoBehaviour
{
    public int[,] levelData = new int[7, 10];

    [SerializeField]
    private LevelButton levelButtonPrefab;

    private Transform prefabParent;
    private HashSet<LevelButton> levelButtonSet;
    private SelectMapManager selectMapManager;

    private void Awake()
    {
        levelButtonSet = new HashSet<LevelButton>();
        selectMapManager = transform.parent.Find("MapScrollView").GetComponent<SelectMapManager>();
        prefabParent = transform.Find("LevelList");

        transform.Find("BackButton").GetComponent<Button>().onClick
            .AddListener(() =>
            {
                gameObject.SetActive(false);
                selectMapManager.gameObject.SetActive(true);
            });

        for (int y = 0; y < levelData.GetLength(0); y++)
        {
            for (int x = 0; x < levelData.GetLength(1); x++)
            {
                levelData[y, x] = x == 0 ? 0 : -1;
            }
        }

        gameObject.SetActive(false);
    }

    public void ChooseMap(int nowMap)
    {
        if (levelButtonSet.Count <= 0)
        {
            for (int i = 0; i < levelData.GetLength(1); i++)
            {
                levelButtonSet.Add(Instantiate(levelButtonPrefab, prefabParent));
            }
        }

        int nowLevel = 1;
        foreach (var item in levelButtonSet)
        {
            item.SetLevelAndStar(nowLevel, levelData[nowMap, nowLevel - 1]);
            nowLevel++;
        }

    }
}
