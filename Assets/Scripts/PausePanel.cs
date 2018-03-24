using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField]
    private Button pauseButton;

    private Animator anim;
    private int isPauseHash = Animator.StringToHash("IsPause");

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    /// <summary>
    /// 点击了继续按钮
    /// </summary>
    public void ClickResumeButton()
    {
        Time.timeScale = 1;
        anim.SetBool(isPauseHash, false);
    }

    public void ClickRetryButton()
    {
        Time.timeScale = 1;
        GameSceneManager.ReLoad();
    }

    public void ClickHomeButton()
    {
        Time.timeScale = 1;
        GameSceneManager.LoadHome();
    }

    /// <summary>
    /// 点击了暂停按钮
    /// </summary>
    public void ClickPauseButton()
    {
        gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        anim.SetBool(isPauseHash, true);
    }





    /// <summary>
    /// pause动画播放完成调用
    /// </summary>
    public void PauseAnimEnd()
    {
        Time.timeScale = 0;
    }


    /// <summary>
    /// resume动画播放完成调用
    /// </summary>
    public void ResumeAnimEnd()
    {
        gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(true);

    }
}
