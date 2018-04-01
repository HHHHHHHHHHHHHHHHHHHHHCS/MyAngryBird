using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    private GameObject failPanel, succeedPanel;

    public GameUIManager Init()
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
        for (int i = 1; i <= star; i++)
        {
            StartCoroutine(ShowStart(i, i * 0.75f));
        }
        succeedPanel.SetActive(true);
    }

    private IEnumerator ShowStart(int i, float time)
    {
        yield return new WaitForSeconds(time);
        succeedPanel.transform.Find(NameTagLayer.starImage + i.ToString()).gameObject
            .SetActive(true);
    }


}
