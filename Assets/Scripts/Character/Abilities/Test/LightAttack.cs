using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAttack : Ability
{
    public LightAttack()
    {
        name = "Light";
        attackDamage = 3;
        accuracy = 1f;
        coolDownTime = 4f;
    }

    public override void useAbility(GameObject target, int buff)
    {
        Enemy targetEnemyComponent = target.GetComponent<Enemy>();
        targetEnemyComponent.TakeDamage(attackDamage + buff);
        setCoolDownTimer();
    }
}
