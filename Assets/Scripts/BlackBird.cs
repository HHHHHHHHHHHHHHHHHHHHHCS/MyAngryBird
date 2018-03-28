using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird
{
    [SerializeField]
    private Sprite boom;

    private HashSet<EnemyUnit> enemySet = new HashSet<EnemyUnit>();



    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(NameTagLayer.t_pig)
            || collision.CompareTag(NameTagLayer.t_block))
        {
            var enemy = collision.GetComponent<EnemyUnit>();
            if(!enemySet.Contains(enemy))
            {
                enemySet.Add(enemy);
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(NameTagLayer.t_pig))
        {
            var pig = collision.GetComponent<EnemyUnit>();
            if (enemySet.Contains(pig))
            {
                enemySet.Remove(pig);
            }
        }
    }

    protected override void UseSkill()
    {
        base.UseSkill();
        if(enemySet.Count>0)
        {
            foreach(var enemy in enemySet)
            {
                enemy.Dead();
            }
        }
        Next();
    }
}
