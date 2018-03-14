using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIManager : MonoBehaviour
{
    private GameObject failPanel, succeedPanel;

    public MainUIManager Init()
    {
        failPanel = transform.Find(NameTagLayer.failPanel).gameObject;
        succeedPanel = transform.Find(NameTagLayer.scceedPanel).gameObject;
        return this;
    }

    public void ShowFailPanel()
    {
        failPanel.SetActive(true);
    }

    public void ShowSucceedPanel(int star = 0)
    {
        for(int i =1;i<=star;i++)
        {
            succeedPanel.transform.Find(NameTagLayer.starImage+i.ToString()).gameObject
                .SetActive(true);
        }
        succeedPanel.SetActive(true);
    }
}
