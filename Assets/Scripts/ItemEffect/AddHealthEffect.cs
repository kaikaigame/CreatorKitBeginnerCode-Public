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
        if (user.Stats.CurrentHealth == user.Stats.stats.health)
            return false;

        StatSystem.StatModifier modifier = new StatSystem.StatModifier();
        modifier.ModifierMode = StatSystem.StatModifier.Mode.Absolute;
        modifier.Stats.health = HealthAmount;

        VFXManager.PlayVFX(VFXType.Healing, user.transform.position);
        user.Stats.ChangeHealth(HealthAmount);

        return true;
    }
}
