using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleristMelee: Ability
{
    public ArtilleristMelee()
    {
        name = "ArtilleristMelee";
        attackDamage = 10;
        accuracy = 1f;
        coolDownTime = 5f;
        animationBool = "isMelee";
    }

    public override void useAbility(GameObject target, int buff)
    {
        Enemy targetEnemyComponent = target.GetComponent<Enemy>();
        targetEnemyComponent.TakeDamage(attackDamage + buff);
        setCoolDownTimer();
    }
}
