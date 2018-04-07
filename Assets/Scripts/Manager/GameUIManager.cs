using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    private GameObject failPanel, succeedPanel,pauseButton;

    public GameUIManager Init()
    {
        failPanel = transform.Find(NameTagLayer.failPanelPath).gameObject;
        succeedPanel = transform.Find(NameTagLayer.scceedPanelPath).gameObject;
        pauseButton=transform.Find(NameTagLayer.pauseButton).gameObject;
        return this;
    }

    public void ShowFailPanel()
    {
        pauseButton.SetActive(false);
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
        succeedPanel.transform.Find(NameTagLayer.starImagePath + i.ToString()).gameObject
            .SetActive(true);
    }

    public void ClickHomeButton()
    {
        GameSceneManager.LoadHome();
    }


    public void ClickNextButton()
    {
        GameGameManager.Instance.LoadNextLevel();
    }

    public void ClickRetryButton()
    {
        GameSceneManager.ReLoad();
    }

}
