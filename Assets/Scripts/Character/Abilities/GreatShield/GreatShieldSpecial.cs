using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatShieldSpecial : Ability
{
    public GreatShieldSpecial()
    {
        name = "GreatShieldSpecial";
        attackDamage = 0;
        accuracy = 1f;
        coolDownTime = 40f;
        isSpecial = true;
    }

    public override void useAbility(GameObject target, int buff)
    {
        Enemy targetEnemyComponent = target.GetComponent<Enemy>();
        targetEnemyComponent.TakeDamage(attackDamage + buff);
        setCoolDownTimer();
    }
}
