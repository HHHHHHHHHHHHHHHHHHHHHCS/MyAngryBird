using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMapManager : MonoBehaviour
{
    public int nowAllStar { get; private set; }
    private int[] mapData;
    private int[] levelStarData;
    private SelectMap[] selectMaps;
    private LevelChooseManager levelChooseManager;

    private void Awake()
    {
        mapData = JsonManager.Instance.ReadNeedStar();
        int[,] levelData = JsonManager.Instance.ReadLevelStar();
        nowAllStar = CalculateStar(levelData);
        levelChooseManager = transform.parent.Find("LevelChooseBg").GetComponent<LevelChooseManager>();
        selectMaps = transform.GetComponentsInChildren<SelectMap>(true);
        int levelMaxStar = levelData.GetLength(1) * 3;
        for (int i = 0; i < selectMaps.Length && i < mapData.Length; i++)
        {
            selectMaps[i].SetStarAndLock(i, levelStarData[i], mapData[i], levelMaxStar, this);
        }
    }

    public int CalculateStar(int[,] levelData)
    {
        int allStar = 0;
        levelStarData = new int[levelData.GetLength(0)];
        for (int i = 0; i < levelStarData.Length; i++)
        {
            levelStarData[i] = 0;
            for (int j = 0;j< levelData.GetLength(1);j++)
            {
                levelStarData[i] += Mathf.Clamp(levelData[i, j], 0, 3);
            }
            allStar += levelStarData[i];
        }
        return allStar;
    }

    public void ChooseMap(int nowMap)
    {
        levelChooseManager.ChooseMap(nowMap);
        gameObject.SetActive(false);
        levelChooseManager.gameObject.SetActive(true);
    }

}
