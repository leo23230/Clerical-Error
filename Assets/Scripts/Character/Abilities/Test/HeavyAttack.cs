using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyAttack : Ability
{
    public HeavyAttack()
    {
        name = "Heavy";
        attackDamage = 6;
        accuracy = 1f;
        coolDownTime = 7f;
    }

    public override void useAbility(GameObject target, int buff)
    {
        Health targetHealthComponent = target.GetComponent<Health>();
        targetHealthComponent.SubtractHealth(attackDamage + buff);
        setCoolDownTimer();
    }
}
