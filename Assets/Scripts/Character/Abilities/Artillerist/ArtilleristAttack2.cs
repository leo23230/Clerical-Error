using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleristAttack2 : Ability
{
    public ArtilleristAttack2()
    {
        name = "Heavy";
        attackDamage = 75;
        accuracy = 1f;
        coolDownTime = 25f;
    }

    public override void useAbility(GameObject target)
    {
        Health targetHealthComponent = target.GetComponent<Health>();
        targetHealthComponent.SubtractHealth(attackDamage);
        setCoolDownTimer();
    }
}
