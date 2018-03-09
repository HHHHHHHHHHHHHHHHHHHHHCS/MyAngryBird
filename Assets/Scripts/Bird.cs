using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private const float maxDistance = 1.5f;

    private Camera mainCamera;
    private Transform rightBranchPos;
    private bool isClick = false;
    private Rigidbody2D rigi;
    private SpringJoint2D springJoint;

    private void Awake()
    {
        mainCamera = Camera.main;
        rigi = GetComponent<Rigidbody2D>();
        springJoint = GetComponent<SpringJoint2D>();
        rightBranchPos = GameObject.Find("Branch_Right").transform;
    }

    private void OnMouseDown()
    {
        isClick = true;
        rigi.isKinematic = true;
    }


    private void OnMouseUp()
    {
        isClick = false;
        rigi.isKinematic = false;
        Invoke("Fly", 0.1f);
    }

    private void Update()
    { 
        if (isClick)
        {
            transform.position = mainCamera.ScreenToWorldPoint(Input.mousePosition)
                - new Vector3(0, 0, mainCamera.transform.position.z);
            if (Vector3.Distance(transform.position, rightBranchPos.position) >= maxDistance)
            {//进行位置限定
                Vector3 vec3 = (transform.position - rightBranchPos.position).normalized * maxDistance 
                    +rightBranchPos.position ;  //单位化*最大长度+初始位置
                transform.position = vec3;
            }

        }
    }

    private void Fly()
    {
        springJoint.enabled = false;
    }
}
