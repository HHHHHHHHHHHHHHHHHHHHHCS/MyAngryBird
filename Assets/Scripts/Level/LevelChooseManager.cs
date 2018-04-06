using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelChooseManager : MonoBehaviour
{
    public int[,] levelData;

    [SerializeField]
    private LevelButton levelButtonPrefab;

    private Transform prefabParent;
    private HashSet<LevelButton> levelButtonSet;
    private SelectMapManager selectMapManager;
    private int nowMap;

    private void Awake()
    {
        //JsonManager.Instance.UpdateLevelStar(0,0,3);
        levelData = JsonManager.Instance.ReadLevelStar();
        levelButtonSet = new HashSet<LevelButton>();
        selectMapManager = transform.parent.Find("MapScrollView").GetComponent<SelectMapManager>();
        prefabParent = transform.Find("LevelList");

        transform.Find("BackButton").GetComponent<Button>().onClick
            .AddListener(() =>
            {
                gameObject.SetActive(false);
                selectMapManager.gameObject.SetActive(true);
            });


        gameObject.SetActive(false);

    }

    public void ChooseMap(int _nowMap)
    {
        nowMap = _nowMap;
        if (levelButtonSet.Count <= 0)
        {
            for (int i = 0; i < levelData.GetLength(1); i++)
            {
                levelButtonSet.Add(Instantiate(levelButtonPrefab, prefabParent));
            }
        }

        int nowLevel = 0;
        foreach (var item in levelButtonSet)
        {
            item.SetLevelAndStar(this, nowLevel, levelData[_nowMap, nowLevel]);
            nowLevel++;
        }
    }

    public void ClickLevelButton(int nowLevel)
    {
        GameGameManager.SetNowMapAndLevel(nowMap, nowLevel);
        GameSceneManager.LoadGamelLoading(nowMap , nowLevel);
    }
}
