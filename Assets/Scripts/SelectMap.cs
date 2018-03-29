using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMap : MonoBehaviour
{
    private int needStar;
    private int maxStar;

    private int nowStar;

    private GameObject startBg;
    private GameObject lockBg;
    private Text starText;
    private Text needStarText;

    public void SetStarAndLock(int _needStar, int _maxStar)
    {
        startBg = transform.Find("StarBg").gameObject;
        lockBg = transform.Find("LockBg").gameObject;
        starText = transform.Find("StarBg/StarText").GetComponent<Text>();
        needStarText = transform.Find("LockBg/NeedStarImage/NeedStarText").GetComponent<Text>();


        needStar = _needStar;
        maxStar = _maxStar;
        SetLock();
    }

    private void SetLock()
    {
        if (nowStar >= needStar)
        {
            GetComponent<Button>().interactable = true;
            startBg.SetActive(true);
            lockBg.SetActive(false);
            SetStarText();
        }
        else
        {
            GetComponent<Button>().interactable = false;
            startBg.SetActive(false);
            lockBg.SetActive(true);
            needStarText.text = needStar.ToString();
        }
    }

    private void SetStarText()
    {
        starText.text = string.Format("{0}/{1}", nowStar, maxStar);
    }
}
