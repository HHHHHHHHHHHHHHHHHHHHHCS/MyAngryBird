using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    private Camera mainCamera;
    private bool isClick = false;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        isClick = true;
    }


    private void OnMouseUp()
    {
        isClick = false;
    }

    private void Update()
    {
        if (isClick)
        {
            transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition+Vector3.forward);
        }
    }
}
