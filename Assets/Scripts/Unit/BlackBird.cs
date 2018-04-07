using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird
{
    private HashSet<EnemyUnit> enemySet = new HashSet<EnemyUnit>();
    private CircleCollider2D boomCollider;

    protected override void Awake()
    {
        base.Awake();
        boomCollider = GetComponents<CircleCollider2D>()[1];
        boomCollider.enabled = false;
    }

    protected override void Fly()
    {
        base.Fly();
        boomCollider.enabled = true;
    }


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
        if (collision.CompareTag(NameTagLayer.t_pig)
            || collision.CompareTag(NameTagLayer.t_block))
        {
            var unit = collision.GetComponent<EnemyUnit>();
            if (enemySet.Contains(unit))
            {
                enemySet.Remove(unit);
            }
        }
    }

    protected override void UseSkill()
    {
        base.UseSkill();
        if(enemySet.Count>0)
        {
            int index= 0;
            EnemyUnit[] units = new EnemyUnit[enemySet.Count];
            foreach (var enemy in enemySet)
            {
                if(enemy)
                {
                    units[index++] = enemy;

                } 
            }
            for(int i=0;i< index;i++)
            {
                units[i].Dead();
            }
        }
        Next();
    }
}
