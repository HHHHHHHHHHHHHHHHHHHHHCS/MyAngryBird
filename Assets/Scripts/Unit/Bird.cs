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
    [SerializeField]
    protected Sprite hurtImage;
    [SerializeField]
    protected Sprite skillUseImage;


    protected bool isClick = false;
    protected Rigidbody2D rigi;
    protected SpringJoint2D springJoint;
    protected bool isUsed;
    protected bool isFlying;
    protected bool isSkillUsed;
    protected bool isHurt;
    protected BirdTrail birdTrail;
    private CircleCollider2D circleCollider;
    protected SpriteRenderer spriteRender;

    protected virtual void Awake()
    {
        if (!mainCamera)
        {
            mainCamera = Camera.main;
        }

        rigi = GetComponent<Rigidbody2D>();
        springJoint = GetComponent<SpringJoint2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        spriteRender = GetComponent<SpriteRenderer>();
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
            GameGameManager.Instance.GameAudioManager.PlayAudio(AudioNames.birdSelect, transform.position);
        }
    }

    protected virtual void OnMouseUp()
    {
        if (!isUsed)
        {
            isClick = false;
            rigi.isKinematic = false;
            isUsed = true;
            isFlying = true;
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
            Next();
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(NameTagLayer.t_pig)
            || collision.collider.CompareTag(NameTagLayer.t_block))
        {
            isHurt = true;
            if(hurtImage)
            {
                spriteRender.sprite = hurtImage;
            }
            collision.gameObject.GetComponent<EnemyUnit>().BirdCrash(rigi);
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

        birdTrail.ShowTrail();
        springJoint.enabled = false;
        Camera.main.GetComponent<CameraFollow>().SetTarget(transform);
        GameGameManager.Instance.GameAudioManager.PlayAudio(AudioNames.birdFly, transform.position);
    }

    protected virtual bool CheckEndFly()
    {
        if (isUsed && rigi.velocity.sqrMagnitude <= 0.2f)
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.down, 20f
                ,LayerMask.GetMask(NameTagLayer.l_border));
            if(ray.distance<=circleCollider.radius+0.1f)
            {
                return true;
            }
            return false;
        }
        return false;
    }

    protected virtual void Next()
    {
        isFlying = false;
        Camera.main.GetComponent<CameraFollow>().SetTarget(null);
        Instantiate(birdDeadEffect, transform.position, Quaternion.identity);
        GameGameManager.Instance.MoveNextBird();
        GameGameManager.Instance.GameAudioManager.PlayAudio(AudioNames.birdCollision01, transform.position);
        Destroy(gameObject);
    }

    protected virtual void UseSkill()
    {
        isSkillUsed = true;
        if(isHurt&&skillUseImage)
        {
            spriteRender.sprite = skillUseImage;
        }
    }
}
