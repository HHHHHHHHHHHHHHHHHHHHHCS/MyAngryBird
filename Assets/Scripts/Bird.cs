using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    protected const float maxDistance = 1.5f;
    protected static Branch rightBranch;
    protected static Branch leftBranch;
    protected static Camera mainCamera;

    [SerializeField]
    protected GameObject birdDeadEffect;


    protected bool isClick = false;
    protected Rigidbody2D rigi;
    protected SpringJoint2D springJoint;
    protected bool isUsed;
    protected bool isFlying;
    protected bool isSkillUsed;
    protected BirdTrail birdTrail;
    private CircleCollider2D circleCollider;

    protected virtual void Awake()
    {
        if (!mainCamera)
        {
            mainCamera = Camera.main;
        }

        rigi = GetComponent<Rigidbody2D>();
        springJoint = GetComponent<SpringJoint2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        birdTrail = transform.GetComponentInChildren<BirdTrail>();
        if (!rightBranch)
        {
            rightBranch = GameObject.Find("Branch/Branch_Right").GetComponent<Branch>();
        }
        if (!leftBranch)
        {
            leftBranch = GameObject.Find("Branch/Branch_Left").GetComponent<Branch>();
        }
        circleCollider.enabled = false;
        rigi.bodyType = RigidbodyType2D.Static;
    }

    protected virtual void OnMouseDown()
    {
        if (!isUsed)
        {
            isClick = true;
            rigi.isKinematic = true;
            MainGameManager.Instance.mainAudioManager.PlayAudio(AudioNames.birdSelect, transform.position);
        }
    }

    protected virtual void OnMouseUp()
    {
        if (!isUsed)
        {
            isClick = false;
            rigi.isKinematic = false;
            isUsed = true;
            Invoke("Fly", 0.1f);
        }
    }

    protected virtual void Update()
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

        if (!isSkillUsed && isFlying && Input.GetMouseButtonDown(0))
        {
            UseSkill();
        }

        if (CheckEndFly())
        {
            isFlying = false;
            Camera.main.GetComponent<CameraFollow>().SetTarget(null);
            Next();
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(NameTagLayer.pig)
            || collision.collider.CompareTag(NameTagLayer.build))
        {
            birdTrail.ClearTrail();
        }
    }

    public virtual void Enable(Vector3? vec3 = null)
    {
        if (vec3 != null)
        {
            transform.position = (Vector3)vec3;
        }
        enabled = true;
        springJoint.enabled = true;
        circleCollider.enabled = true;
        rigi.bodyType = RigidbodyType2D.Dynamic;
    }

    protected virtual void Fly()
    {
        if (rightBranch)
        {
            rightBranch.Disable();
        }
        if (leftBranch)
        {
            leftBranch.Disable();
        }
        isFlying = true;
        birdTrail.ShowTrail();
        springJoint.enabled = false;
        Camera.main.GetComponent<CameraFollow>().SetTarget(transform);
        MainGameManager.Instance.mainAudioManager.PlayAudio(AudioNames.birdFly, transform.position);
    }

    protected virtual bool CheckEndFly()
    {
        if (isUsed && rigi.velocity.sqrMagnitude <= 0.1f)
        {

            return true;
        }
        return false;
    }

    protected virtual void Next()
    {
        Instantiate(birdDeadEffect, transform.position, Quaternion.identity);
        MainGameManager.Instance.MoveNextBird();
        MainGameManager.Instance.mainAudioManager.PlayAudio(AudioNames.birdCollision01, transform.position);
        Destroy(gameObject);
    }

    protected virtual void UseSkill()
    {
        isSkillUsed = true;
    }
}
