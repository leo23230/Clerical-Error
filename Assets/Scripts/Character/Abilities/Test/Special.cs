using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : Ability
{
    public Special()
    {
        name = "Special";
        attackDamage = 10;
        accuracy = 1f;
        coolDownTime = 20f;
    }

    public override void useAbility(GameObject target, int buff)
    {
        Health targetHealthComponent = target.GetComponent<Health>();
        targetHealthComponent.SubtractHealth(attackDamage + buff);
        setCoolDownTimer();
    }
}
