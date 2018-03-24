using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : EnemyUnit
{
    protected override void Dead()
    {

        MainGameManager.Instance.mainAudioManager.PlayAudio(AudioNames.woodDestoryed, transform.position);
        base.Dead();
    }
}
