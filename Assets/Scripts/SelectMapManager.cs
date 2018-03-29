using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMapManager : MonoBehaviour
{
    private int[,] mapData = new int[7, 2] { { 0,10}, { 5,20}, { 15,30}, { 30,40}, { 50,50}
        , {75,60 }, { 105,70} };
    private SelectMap[] selectMaps;

    private void Awake()
    {
        selectMaps = transform.GetComponentsInChildren<SelectMap>(true);
        for(int i= 0;i<selectMaps.Length&&i<mapData.Length;i++)
        {
            selectMaps[i].SetStarAndLock(mapData[i,0], mapData[i,1]);
        }
    }
}
