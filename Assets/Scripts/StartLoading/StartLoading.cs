using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLoading : MonoBehaviour
{
	private void Awake ()
    {
        GameSceneManager.AsyncLoadMapLevelScene();
    }
}
