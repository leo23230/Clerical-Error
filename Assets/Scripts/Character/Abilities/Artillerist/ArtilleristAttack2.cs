using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleristAttack2 : Ability
{
    public ArtilleristAttack2()
    {
        name = "Heavy";
        attackDamage = 60;
        accuracy = 1f;
        coolDownTime = 25f;
    }

    public override void useAbility(GameObject target, int buff)
    {
        Enemy targetEnemyComponent = target.GetComponent<Enemy>();
        targetEnemyComponent.TakeDamage(attackDamage + buff);
        setCoolDownTimer();
    }
}
