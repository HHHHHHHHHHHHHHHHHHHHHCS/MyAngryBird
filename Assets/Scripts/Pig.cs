using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : EnemyUnit
{
    [SerializeField]
    protected GameObject pigDeadEffect;

    protected override void Hurt()
    {
        MainGameManager.Instance.mainAudioManager.PlayAudio(AudioNames.pigCollision01, transform.position);
        base.Hurt();
    }

    public override void Dead()
    {
        var effect = Instantiate(pigDeadEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        MainGameManager.Instance.mainAudioManager.PlayAudio(AudioNames.pigCollision02,transform.position);
        MainGameManager.Instance.RemovePig(this);
        base.Dead();
    }
}
