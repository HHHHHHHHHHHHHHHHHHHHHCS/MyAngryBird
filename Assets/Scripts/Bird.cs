using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private const float maxDistance = 1.5f;
    private static Branch rightBranch;
    private static Branch leftBranch;
    private static Camera mainCamera;

    [SerializeField]
    private GameObject birdDeadEffect;

    private bool isClick = false;
    private Rigidbody2D rigi;
    private SpringJoint2D springJoint;
    private bool isUsed;
    private BirdTrail birdTrail;

    private void Awake()
    {
        if (!mainCamera)
        {
            mainCamera = Camera.main;
        }

        rigi = GetComponent<Rigidbody2D>();
        springJoint = GetComponent<SpringJoint2D>();
        birdTrail = transform.GetComponentInChildren<BirdTrail>();
        if (!rightBranch)
        {
            rightBranch = GameObject.Find("Branch/Branch_Right").GetComponent<Branch>();
        }
        if (!leftBranch)
        {
            leftBranch = GameObject.Find("Branch/Branch_Left").GetComponent<Branch>();
        }

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
            if (Vector3.Distance(transform.position, rightBranch.transform.position) >= maxDistance)
            {//进行位置限定
                Vector3 vec3 = (transform.position - rightBranch.transform.position).normalized * maxDistance
                    + rightBranch.transform.position;  //单位化*最大长度+初始位置
                transform.position = vec3;
            }
            if (rightBranch)
            {
                rightBranch.DrawLine(transform);
            }
            if (leftBranch)
            {
                leftBranch.DrawLine(transform);
            }
        }

        if (EndFly())
        {
            Next();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag(NameTagLayer.pig)
            || collision.collider.CompareTag(NameTagLayer.build))
        {
            birdTrail.ClearTrail();
        }
    }

    public void Enable(Vector3? vec3 = null)
    {
        if (vec3 != null)
        {
            transform.position = (Vector3)vec3;
        }
        enabled = true;
        springJoint.enabled = true;
    }

    private void Fly()
    {
        if (rightBranch)
        {
            rightBranch.Disable();
        }
        if (leftBranch)
        {
            leftBranch.Disable();
        }
        birdTrail.ShowTrail();
        springJoint.enabled = false;
        isUsed = true;
    }

    private bool EndFly()
    {
        if (isUsed && rigi.velocity.sqrMagnitude <= 0.1f)
        {
            return true;
        }
        return false;
    }

    private void Next()
    {
        Instantiate(birdDeadEffect, transform.position, Quaternion.identity);
        MainGameManager.Instance.MoveNextBird();
        Destroy(gameObject);
    }


}
