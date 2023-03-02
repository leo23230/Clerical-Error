using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttack : Ability
{
    public HeavyAttack()
    {
        name = "HeavyAttack";
        attackDamage = 10;
        accuracy = 1f;
        coolDownTime = 2f;
    }

    public override void useAbility(GameObject target)
    {
        Health targetHealthComponent = target.GetComponent<Health>();
        targetHealthComponent.SubtractHealth(attackDamage);
        setCoolDownTimer();
    }
}
