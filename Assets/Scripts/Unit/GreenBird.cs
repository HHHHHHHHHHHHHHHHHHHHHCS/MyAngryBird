using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird : Bird
{
    protected override void UseSkill()
    {
        base.UseSkill();
        rigi.velocity = new Vector2(-1.5f*rigi.velocity.x , rigi.velocity.y);
    }
}
