using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatorKitCode;

public class SpeedUpEffect : UsableItem.UsageEffect
{
    //加速
    public float duration = 15f;
    public int agilityChange = 5;
    public Sprite effectSprite;

    public override bool Use(CharacterData user)
    {
        StatSystem.StatModifier modifier = new StatSystem.StatModifier();
        modifier.ModifierMode = StatSystem.StatModifier.Mode.Absolute;
        modifier.Stats.agility = agilityChange;

        VFXManager.PlayVFX(VFXType.FireEffect, user.transform.position);
        user.Stats.AddTimedModifier(modifier, duration, "AgilityAdd", effectSprite);

        return false;
    }
}
