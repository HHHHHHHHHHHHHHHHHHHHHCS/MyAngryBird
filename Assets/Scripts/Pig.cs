using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : EnemyUnit
{
    [SerializeField]
    protected GameObject pigDeadEffect;


    protected override void Dead()
    {
        base.Dead();
        var effect = Instantiate(pigDeadEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);

        MainGameManager.Instance.RemovePig(this);
    }
}
