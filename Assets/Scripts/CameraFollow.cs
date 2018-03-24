using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private Vector3 offestPos;
    private Vector3 startPos;
    private float backSpeed = 32f;

    private void Awake()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (target)
        {
            Vector3 vec3 = transform.position;
            vec3.x = Mathf.Clamp(target.position.x + offestPos.x, 0, 15.8f);
            transform.position = vec3;
        }
        else
        {
            if (transform.position != startPos)
            {
                Vector3 vec3 = transform.position;
                vec3.x -= (backSpeed * Time.deltaTime);
                vec3.x = Mathf.Clamp(vec3.x, 0, 15.8f);
                transform.position = vec3;
            }
        }
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
        if (_target)
        {
            offestPos = transform.position - _target.position;
        }
    }
}
