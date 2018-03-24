using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    protected const float sqrMaxSpeed = 64f;//需要根号
    protected const float sqrMinSpeed = 16f;//需要根号

    [SerializeField]
    protected Sprite hurtSprite;
    [SerializeField]
    protected GameObject score;


    protected bool isHurt = false;


    protected SpriteRenderer sprite;

    protected virtual void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.sqrMagnitude > sqrMaxSpeed)
        {
            //直接死亡
            Dead();
        }
        else if (collision.relativeVelocity.sqrMagnitude < sqrMinSpeed)
        {
            //直接不管
        }
        else
        {
            if (!isHurt)
            {
                //让它受伤
                Hurt();
            }
            else
            {
                Dead();
            }
        }
    }

    protected virtual void Hurt()
    {
        sprite.sprite = hurtSprite;
        isHurt = true;
    }

    protected virtual void Dead()
    {
        Destroy(gameObject);
        var score = Instantiate(this.score, transform.position + Vector3.up, Quaternion.identity);
        Destroy(score, 2f);
    }
}
