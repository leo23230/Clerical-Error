using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleristSpecial : Ability
{
    public ArtilleristSpecial()
    {
        name = "Special";
        attackDamage = 100;
        accuracy = 1f;
        coolDownTime = 30f;
    }

    public override void useAbility(GameObject target)
    {
        Health targetHealthComponent = target.GetComponent<Health>();
        targetHealthComponent.SubtractHealth(attackDamage);
        setCoolDownTimer();
    }
}
