using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour
{
    [SerializeField]
    private Sprite hurtSprite;
    [SerializeField]
    private GameObject pigDeadEffect;
    [SerializeField]
    private GameObject pigScore;

    private const float sqrMaxSpeed = 64f;//需要根号
    private const float sqrMinSpeed = 16f;//需要根号
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
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
            //让它受伤
            sprite.sprite = hurtSprite;
        }
    }

    private void Dead()
    {
        //直接死亡
        Destroy(gameObject);
        var effect = Instantiate(pigDeadEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        var score = Instantiate(pigScore, transform.position+Vector3.up, Quaternion.identity);
        Destroy(score, 2f);
    }
}
