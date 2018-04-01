using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : EnemyUnit
{
    public override void Dead()
    {
        GameGameManager.Instance.GameAudioManager.PlayAudio(AudioNames.woodDestoryed, transform.position);
        base.Dead();
    }
}
