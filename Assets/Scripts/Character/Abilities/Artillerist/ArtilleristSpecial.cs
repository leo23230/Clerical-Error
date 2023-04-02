using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleristSpecial : Ability
{
    public ArtilleristSpecial()
    {
        name = "Special";
        attackDamage = 80;
        accuracy = 1f;
        coolDownTime = 30f;
    }

    public override void useAbility(GameObject target, int buff)
    {
        Health targetHealthComponent = target.GetComponent<Health>();
        targetHealthComponent.SubtractHealth(attackDamage + buff);
        setCoolDownTimer();
    }
}
