using UnityEngine;

public abstract class Ability
{
    public string name;

    public int attackDamage;

    public float accuracy;

    public float coolDownTime;

    public float coolDown = 0f;

    public abstract void useAbility(GameObject target, int buff);

    public void setCoolDownTimer()
    {
        coolDown = coolDownTime;
    }
    public void updateCoolDownTimer() {
        if(coolDown > 0f)
        {
            coolDown -= Time.deltaTime;
        }
    }

    public bool AbilityIsReady()
    {
        return coolDown <= 0f;
    }
}
