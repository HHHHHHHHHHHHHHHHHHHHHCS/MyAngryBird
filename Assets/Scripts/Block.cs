using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : EnemyUnit
{
    public override void Dead()
    {
        MainGameManager.Instance.mainAudioManager.PlayAudio(AudioNames.woodDestoryed, transform.position);
        base.Dead();
    }
}
