using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Bird
{


    protected override void UseSkill()
    {
        base.UseSkill();
        rigi.velocity = new Vector2(2 * rigi.velocity.x, rigi.velocity.y);
    }
}
