using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Bird
{

    protected override void UseSkill()
    {
        base.UseSkill();
        rigi.velocity *= 1.5f;
    }
}
