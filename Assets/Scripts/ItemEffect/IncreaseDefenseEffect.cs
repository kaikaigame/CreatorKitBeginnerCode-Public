using System.Collections;
using System.Collections.Generic;
using CreatorKitCode;
using UnityEngine;

public class IncreaseDefenseEffect : UsableItem.UsageEffect
{
    public float Duration = 10.0f;
    public int DefenseChange = 5;
    public Sprite EffectSprite;
    
    public override bool Use(CharacterData user)
    {
        StatSystem.StatModifier modifier = new StatSystem.StatModifier();
        modifier.ModifierMode = StatSystem.StatModifier.Mode.Absolute;
        modifier.Stats.defense = DefenseChange;
        
        VFXManager.PlayVFX(VFXType.Stronger, user.transform.position);        
        user.Stats.AddTimedModifier(modifier, Duration, "DefenseAdd", EffectSprite);
        
        return true;
    }
}
