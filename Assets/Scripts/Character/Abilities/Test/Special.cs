using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : Ability
{
    public Special()
    {
        name = "Special";
        attackDamage = 20;
        accuracy = 1f;
        coolDownTime = 20f;
    }

    public override void useAbility(GameObject target)
    {
        Health targetHealthComponent = target.GetComponent<Health>();
        targetHealthComponent.SubtractHealth(attackDamage);
        setCoolDownTimer();
    }
}
