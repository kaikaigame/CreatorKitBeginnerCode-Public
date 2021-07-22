using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatorKitCode;

public class AddHealthEffect : UsableItem.UsageEffect
{
    //加血
    public int HealthAmount;
    public override bool Use(CharacterData user)
    {
        user.Stats.ChangeHealth(HealthAmount);
        return true;
    }
}
