using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttack : Ability
{
    public HeavyAttack()
    {
        name = "Heavy";
        attackDamage = 10;
        accuracy = 1f;
        coolDownTime = 5f;
    }

    public override void useAbility(GameObject target)
    {
        Health targetHealthComponent = target.GetComponent<Health>();
        targetHealthComponent.SubtractHealth(attackDamage);
        setCoolDownTimer();
    }
}
