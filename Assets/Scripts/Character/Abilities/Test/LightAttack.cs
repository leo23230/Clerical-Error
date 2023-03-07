using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttack : Ability
{
    public LightAttack()
    {
        name = "Light";
        attackDamage = 5;
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
