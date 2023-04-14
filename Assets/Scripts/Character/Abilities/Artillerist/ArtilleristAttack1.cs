using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleristAttack1 : Ability
{
    public ArtilleristAttack1()
    {
        name = "Light";
        attackDamage = 40;
        accuracy = 1f;
        coolDownTime = 20f;
    }

    private void updateCoolDownBar(CooldownBarManager CDBarManager)
    {
    }

    public override void useAbility(GameObject target, int buff)
    {
        Enemy targetEnemyComponent = target.GetComponent<Enemy>();
        targetEnemyComponent.TakeDamage(attackDamage + buff);
        setCoolDownTimer();
    }
}
