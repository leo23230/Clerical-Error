using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Linq;

public class CharacterStateManager : MonoBehaviour
{
    //inspector stuff//

    //effects//
    [SerializeField] public GameObject hitEffect;
    [SerializeField] public GameObject deathEffect;
    [SerializeField] public GameObject healEffect;
    [SerializeField] public GameObject speedEffect;
    [SerializeField] public GameObject damageEffect;
    [SerializeField] public GameObject greatShieldEffect;

    //state manager stuff//
    [HideInInspector] public CharacterIdleState idleState = new CharacterIdleState();
    [HideInInspector] public CharacterWalkState walkState = new CharacterWalkState();
    [HideInInspector] public CharacterAttackState attackState = new CharacterAttackState();
    [HideInInspector] public CharacterDeadState deadState = new CharacterDeadState();

    //stats//
    [HideInInspector] public Character character;
    [HideInInspector] public CharacterBaseState currentState;
    [HideInInspector] public float moveSpeed;
    [HideInInspector] public int damageBuff = 0;
    [HideInInspector] public float abilityReadyCooldown;
    [HideInInspector] public bool isInAltState;
    [HideInInspector] public Ability chosenAbility = null;
    [HideInInspector] public int multiAbilityUseCounter = 0;
    //components//
   [HideInInspector] public Animator animator;
    [HideInInspector] public Inventory inventory;

    //other//
    [HideInInspector] public GameObject target;
    [HideInInspector] public GameObject[] enemies;
    [HideInInspector] public List<Ability> abilities = new List<Ability>();
    [HideInInspector] public Ability backupAbility;
    [HideInInspector] public Vector2 startingPos = new Vector2();
    [HideInInspector] public Vector3 effectAnchorPos;
    [HideInInspector] public GameObject characterCanvas;
    [HideInInspector] public Inventory inventoryComponent;
    [HideInInspector] public GameObject hoverLight;

    private GameObject spellReadyEffect;
    private void Awake()
    {
        //the character component is repsonible for storing
        //the character stats from the scriptable object, and 
        //relevant component data that the state manager will use

        //set character stats
        character = gameObject.GetComponent<Character>();

        inventory = GameObject.Find("Player").GetComponent<Inventory>();

        effectAnchorPos = transform.Find("EffectAnchor").transform.position;

        characterCanvas = transform.Find("CharacterCanvas").gameObject;

        hoverLight = transform.Find("HoverLight").gameObject;

        hoverLight.SetActive(false);

        inventoryComponent = GameObject.Find("Player").GetComponent<Inventory>();

        spellReadyEffect = GameObject.Find("SpellReadyEffect");

        target = SelectEnemy();
    }

    private void OnEnable()
    {
        StaticEventHandler.EnemyDiedEvent += ChooseEnemyUponEnemyDeath;
        StaticEventHandler.EnemySpawnedEvent += ChooseEnemyUponEnemySpawn;
    }

    private void OnDisable()
    {
        StaticEventHandler.EnemyDiedEvent -= ChooseEnemyUponEnemyDeath;
        StaticEventHandler.EnemySpawnedEvent -= ChooseEnemyUponEnemySpawn;
    }

    void Start()
    {
        //stat cahce//
        animator = character.animator;
        moveSpeed = character.speed;
        abilityReadyCooldown = character.abilityReadyCooldown;

        startingPos = new Vector2(transform.position.x, transform.position.y);

        InstantiateAbilities();

        idleState.EnterState(this);

        //after all characters have grabbed a reference to this in Awake, we will disable in Start
        if(spellReadyEffect.activeSelf)spellReadyEffect.SetActive(false);
    }
    void Update()
    {
        currentState.UpdateState(this);

        //always need to update cool down timers
        UpdateCoolDownTimers();

        //No matter what state the character is in
        if (character.healthComponent.GetHealth() <= 0)
        {
            if(currentState != deadState)
            {
                deadState.EnterState(this);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (inventoryComponent.hasPreparedSpell())
            {
                if(inventoryComponent.preparedSpell[0] == "Rune_ TargetCharacters")
                {
                    RecieveSpellEffects(inventoryComponent.preparedSpell);
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && NotDead() && inventory.GetHandItem() != null)
        {
            StaticEventHandler.CallConsumableUsedEvent(this);
        }
        if (Input.GetMouseButtonDown(0) && NotDead() && inventory.hasPreparedSpell())
        {
            if (inventory.GetPreparedSpell()[0] == "Rune_ SingleTarget")
            {  
                RecieveSpellEffects(inventory.GetPreparedSpell());            
            }
        }
        else if (Input.GetMouseButtonDown(0) && inventory.hasPreparedSpell())
        {
            if(inventory.GetPreparedSpell()[0] == "Rune_ Res")
            {
                animator.Play("Res");
            }
        }
    }

    private void OnMouseOver()
    {
        if (currentState != deadState && (inventoryComponent.hasHandItem() || (inventory.GetPreparedSpell()[0] == "Rune_ SingleTarget" || inventory.GetPreparedSpell()[0] == "Rune_ Res")))
        {
            hoverLight.SetActive(true);
        }
        else if(currentState == deadState && inventoryComponent.preparedSpell[0] == "Rune_ Res")
        {
            hoverLight.SetActive(true);
        }
        else
        {
            if (hoverLight.activeSelf) hoverLight.SetActive(false);
        }
    }
    private void OnMouseExit()
    {
        if(hoverLight.activeSelf) hoverLight.SetActive(false);
    }

    public GameObject SelectEnemy() {
        GameObject target;
        List<GameObject> aliveEnemies = findAliveEnemies();
        if(aliveEnemies.Count > 1)
        {
            List<GameObject> closestAliveEnemies = aliveEnemies.OrderBy(x => Vector2.Distance(this.transform.position, x.transform.position)).ToList();

            target = closestAliveEnemies[0];
        }
        else if (aliveEnemies.Count == 1)
        {
            target = aliveEnemies[0];
        }
        else
        {
            target = null;
        }
        
        return target;
    }

    public List<GameObject> findAliveEnemies()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        List<GameObject> aliveEnemies = new List<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            if (enemy.GetComponent<Health>().GetHealth() > 0)
            {
                if (!aliveEnemies.Contains(enemy))
                {
                    aliveEnemies.Add(enemy);
                }
            }
        }

        return aliveEnemies;
    }

    public void ChooseEnemyUponEnemyDeath(EnemyDiedEventArgs eventArgs)
    {
        target = SelectEnemy();
    }

    public void ChooseEnemyUponEnemySpawn(EnemySpawnedEventArgs eventArgs)
    {
        target = SelectEnemy();
    }


    public bool CharacterIsWithinRange()
    {
        if(target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);

            bool isWithinRange = distanceToTarget >= character.minDistance && distanceToTarget <= character.maxDistance;

            return isWithinRange;
        }
        else
        {
            return true;
        }
      
    }

    public bool UseBackupAbility()
    {
        if (backupAbility.AbilityIsReady())
        {
            backupAbility.useAbility(target, damageBuff);

            //start animation
            if (animator != null) animator.SetBool("isMelee", true);

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ChooseAnAbility()
    {
        //collect a list of ready abilities
        List<Ability> readyAbilities = new List<Ability>();

        //first we add only the readied abilities to a list
        foreach (Ability ability in abilities)
        {
            if (ability.AbilityIsReady()) readyAbilities.Add(ability);
        }

        //we first check to make sure there are more than 0 readied abilities
        if (readyAbilities.Count <= 0) 
        {
            return false;
        } 
        //if there is one, we just pick that ability, if there are more than one in that moment pick randomly
        else if (readyAbilities.Count == 1)
        {
            chosenAbility = readyAbilities[0];
            //chosenAbility.useAbility(target, damageBuff);

            //start animation
            if (animator != null) 
            {
                if (chosenAbility.isSpecial)
                {
                    animator.SetBool("isSwitching", true);
                    StartCoroutine(AlternateState(20));
                }
                else
                {
                    if (isInAltState)
                    {
                        animator.SetBool("isAttackingAlt", true);
                    }
                    else
                    {
                        animator.SetBool("isAttacking", true);
                    }
                    
                }
            }
            

            return true;
        }
        else
        {
            int random = UnityEngine.Random.Range(0, readyAbilities.Count);
            chosenAbility = readyAbilities[random];
            //chosenAbility.useAbility(target, damageBuff);

            //start animation
            if (animator != null)
            {
                if (chosenAbility.isSpecial)
                {
                    animator.SetBool("isSwitching", true);
                    StartCoroutine(AlternateState(20));
                }
                else
                {
                    if (isInAltState)
                    {
                        animator.SetBool("isAttackingAlt", true);
                    }
                    else
                    {
                        animator.SetBool("isAttacking", true);
                    }
                }
            }

            return true;
        }
    }

    public void UseChosenAbility()
    {
        chosenAbility.useAbility(target, damageBuff);
        chosenAbility = null;
    }
    public void UseChosenMultiAbility(int _numOfAttacks)
    {
        if(multiAbilityUseCounter < _numOfAttacks)
        {
            chosenAbility.useAbility(target, damageBuff);
            multiAbilityUseCounter += 1;
        }
        else
        {
            multiAbilityUseCounter = 0;
            chosenAbility = null;
        }
    }

    public void InstantiateAbilities()
    {
        foreach (String abilityName in character.abilities)
        {
            //Finds the type that matches the ability name
            System.Type abilityType = Type.GetType(abilityName);

            //creates an instance of the ability (type cast as Ability so methods can be used)
            Ability abilityInstance = (Ability) Activator.CreateInstance(abilityType);

            //add the ability to the list
            abilities.Add(abilityInstance);
        }

        //if we have a backup attack we need to instantiate it and store it
        if (character.hasBackupAbility)
        {
            //Finds the type that matches the ability name
            System.Type abilityType = Type.GetType(character.backupAbility);

            //creates an instance of the ability (type cast as Ability so methods can be used)
            Ability abilityInstance = (Ability)Activator.CreateInstance(abilityType);

            //add the ability to the list
            backupAbility = abilityInstance;
        }
    }

    public void UpdateCoolDownTimers()
    {
        foreach (Ability ability in abilities)
        {
            ability.updateCoolDownTimer();
        }
        if(character.hasBackupAbility)backupAbility.updateCoolDownTimer();
    }

    public void FlipSprite(string dir)
    {
        float dirNumber = 1f;
        if (dir == "left") dirNumber = -1f;
        else if (dir == "right") dirNumber = 1f;

        if (character.sprite != null)
        {
            character.sprite.transform.localScale = new Vector3(dirNumber, character.sprite.transform.localScale.y, character.sprite.transform.localScale.z);
        }
        else
        {
            //super temporary//
            transform.localScale = new Vector3(dirNumber, transform.localScale.y, transform.localScale.z);
            var canvasScale = transform.Find("CharacterCanvas").localScale;
            transform.Find("CharacterCanvas").localScale = new Vector3(dirNumber/1000f, canvasScale.y, canvasScale.z);
        }
    }

    public void DamagePlayer(float _amt)
    {
        //we want to damage the player and cause a damage effect

        _amt *= character.armorClass;

        int intAmt = Mathf.RoundToInt(_amt);

        character.healthComponent.SubtractHealth(intAmt);

        InstantiateEffectPrefab(hitEffect);

        character.alertMonobehaviour.UpdateSprite(character.healthComponent.GetHealth());

    }

    public GameObject InstantiateEffectPrefab(GameObject _prefab)
    {
        GameObject effectObject = Instantiate(_prefab);

        //float yOffset = 2.0f;

        Vector3 newPos = new Vector3(transform.position.x + effectAnchorPos.x, transform.position.y + effectAnchorPos.y, transform.position.z);

        //set pos to middle of character
        effectObject.transform.localPosition = newPos;

        //make sure the effect follows the character
        effectObject.transform.SetParent(transform);
        //spawn effect prefab

        return effectObject;
    }

    public void Heal(int _amt)
    {
        character.healthComponent.AddHealth(_amt);

        character.alertMonobehaviour.UpdateSprite(character.healthComponent.GetHealth());

        InstantiateEffectPrefab(healEffect);
    }

    IEnumerator ChangeSpeed(int _amt, float time)
    {
        moveSpeed += _amt;
        GameObject effectObject = InstantiateEffectPrefab(speedEffect);

        yield return new WaitForSeconds(time);

        moveSpeed -= _amt;
        Destroy(effectObject);

        yield break;
    }

    IEnumerator ChangeDamage(int _amt, float time)
    {
        damageBuff = _amt;
        GameObject effectObject = InstantiateEffectPrefab(damageEffect);

        yield return new WaitForSeconds(time);

        damageBuff -= _amt;
        Destroy(effectObject);

        yield break;
    }

    IEnumerator HealOverTime(int _amt, float time)
    {
        float count = 0f;
        GameObject effectObject = InstantiateEffectPrefab(healEffect);

        while(count <= time)
        {
            Heal(_amt);
            yield return new WaitForSeconds(time / time);
            count += 1;
        }

        //Destroy(effectObject);

        yield break;
    }

    //THIS IS SUPER SLOPPY TEMPORARY AND WILL BE REPLACED BY A PROPER EVENT
    IEnumerator TimedSpellReset()
    {
        yield return new WaitForSeconds(0.2f);

        inventory.UsePreparedSpell();
        spellReadyEffect.SetActive(false);
    }

    public void StatBoost(ItemDetailsSO item)
    {
        if(item.itemName == "Coppabloom Tea")
        {
            Heal(40);
        }
        if(item.itemName == "Papariko Insence")
        {
            StartCoroutine(ChangeSpeed(4, 4f));
        }
        if (item.itemName == "Slayer Stew")
        {
            StartCoroutine(ChangeDamage(10, 10f));
        }
        if (item.itemName == "Herbal Salve")
        {
            StartCoroutine(HealOverTime(10, 8f));
        }
    }

    public void RecieveSpellEffects(List<string> _spell)
    {
        int healAmt = 10;
        int speedAmt = 2;
        int damageAmt = 5;
        int harmAmt = 5;

        int healCount = 0;
        int speedCount = 0;
        int damageCount = 0;
        int harmCount = 0;

        foreach(string rune in _spell)
        {
            if(rune == "Rune_ Res")
            {
                animator.Play("Res");
            }
            if (rune == "Rune_ Heal")
            {
                healCount += 1;
            }
            else if (rune == "Rune_ Speed")
            {
                speedCount += 1;
            }
            else if (rune == "Rune_ Damage")
            {
                damageCount += 1;
            }
            else if (rune == "Rune_ Harm")
            {
                harmCount += 1;
            }
        }

        if (healCount > 0) Heal(healAmt * healCount);
        if (speedCount > 0) StartCoroutine(ChangeSpeed(speedAmt*speedCount, 4f));
        if (damageCount > 0) StartCoroutine(ChangeDamage(damageAmt*damageCount, 10f));
        if (harmCount > 0) DamagePlayer(harmAmt * harmCount);

        StartCoroutine(TimedSpellReset());
    }

    public bool IsPlayingLayer(int layerIndex)
    {
        return character.animator.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime % 1.0f < 1.0f;
    }

    public async void SwitchAnimation(string newAnimationBool)
    {
        //we want to wait until the animator is finished with it's current animation
        while (IsPlayingLayer(0))
        {
            await Task.Yield();
        }

        character.animator.SetBool(newAnimationBool, true);
    }

    public bool NotDead()
    {
        return currentState != deadState;
    }

    //boolean to determine which set of walking animations to use
    //isAltState
    //isRunningAlt
    //isAttackingAlt
    //change character stats in coroutine

    public IEnumerator AlternateState (float duration)
    {
        //this keeps us in the alt branch of the animator
        animator.SetBool("isAlt", true);

        isInAltState = true;
        character.armorClass = 0.2f;
        character.speed -= 1;
        yield return new WaitForSeconds(duration);
        character.armorClass = character.characterDetails.characterArmorClass;
        character.speed += 1;
        isInAltState = false;
        animator.SetBool("isAlt", false);
    }

    public void ResCharacter()
    {
        character.healthComponent.AddHealth(51);
        characterCanvas.SetActive(true);
        animator.SetBool("isDead", false);
        idleState.EnterState(this);

        StartCoroutine(TimedSpellReset());
    }

}
