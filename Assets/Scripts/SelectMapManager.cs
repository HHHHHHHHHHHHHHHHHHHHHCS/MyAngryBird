using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMapManager : MonoBehaviour
{

    private int[,] mapData = new int[7, 2] { { 0,30}, { 15,30}, { 30,30}, { 45,30}, { 60,30}
        , {75,30 }, { 90,30} };
    private SelectMap[] selectMaps;
    private LevelChooseManager levelChooseManager;

    private void Awake()
    {
        levelChooseManager = transform.parent.Find("LevelChooseBg").GetComponent<LevelChooseManager>();
        selectMaps = transform.GetComponentsInChildren<SelectMap>(true);
        for (int i = 0; i < selectMaps.Length && i < mapData.Length; i++)
        {
            selectMaps[i].SetStarAndLock(i, mapData[i, 0], mapData[i, 1], this);
        }
    }

    public void ChooseMap(int nowMap)
    {
        levelChooseManager.ChooseMap(nowMap);
        gameObject.SetActive(false);
        levelChooseManager.gameObject.SetActive(true);
    }

}
