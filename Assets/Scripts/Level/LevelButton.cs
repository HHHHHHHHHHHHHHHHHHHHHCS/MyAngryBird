using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public void SetLevelAndStar(LevelChooseManager levelManager, int level, int starCount)
    {
        var levelText = transform.Find("LevelText").GetComponent<Text>();
        var level_Star = transform.Find("Level_Star");
        var level_Lock = transform.Find("Level_Lock");
        if (starCount < 0)
        {
            level_Lock.gameObject.SetActive(true);
        }
        else
        {
            levelText.text = (level+1).ToString();
            level_Star.gameObject.SetActive(true);
            for (int i = 1; i <= starCount; i++)
            {
                level_Star.Find("Star" + i).gameObject.SetActive(true);
            }
        }
        GetComponent<Button>().onClick.AddListener(
            ()=> { levelManager.ClickLevelButton(level); });
    }

}
