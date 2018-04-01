using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    [SerializeField]
    private bool isRight;

    private LineRenderer line;
    private Transform linePos;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        linePos = transform.Find("LinePos");
    }

    public void DrawLine(Transform target)
    {
        line.enabled = true;
        line.SetPosition(0, linePos.position);
        line.SetPosition(1, target.position);
    }

    public void Disable()
    {
        line.enabled = false;
    }
}
