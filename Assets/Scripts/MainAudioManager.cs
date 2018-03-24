using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainAudioManager : MonoBehaviour
{
    [SerializeField]
    protected AudioClip[] audioArray;


    public MainAudioManager Init()
    {
        return this;
    }

    public void PlayAudio(string audioName,Vector3 pos)
    {
        foreach (var item in audioArray)
        {
            if (item.name == audioName)
            {
                AudioSource.PlayClipAtPoint(item, pos);
                return;
            }
        }
    }
}
