using UnityEngine;

public abstract class Ability
{
    public string name;

    public int attackDamage;

    public float accuracy;

    public float coolDownTime;

    public float coolDown = 0f;

    public abstract void useAbility();

    public void setCoolDown()
    {
        coolDown = coolDownTime;
    }
    public void coolDownTimerCount() {
        if(coolDown > 0f)
        {
            coolDownTime -= Time.deltaTime;
        }
    }
}
