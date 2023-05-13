using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyStateManager : MonoBehaviour
{

    //state manager stuff//
    [HideInInspector] public EnemyIdleState idleState = new EnemyIdleState();
    [HideInInspector] public EnemyMoveState moveState = new EnemyMoveState();
    [HideInInspector] public EnemyAttackState attackState = new EnemyAttackState();
    [HideInInspector] public EnemyDeadState deadState = new EnemyDeadState();

    //stats//
    [HideInInspector] public Enemy enemy;
    [HideInInspector] public EnemyBaseState currentState;
    [HideInInspector] public float moveSpeed;

    //components//
    [HideInInspector] public Animator animator;

    //other//
    [HideInInspector] public GameObject target;
    [HideInInspector] public GameObject[] enemys;
    [HideInInspector] public Vector3 effectAnchorPos;
    [HideInInspector] public Vector2 startingPos = new Vector2();
    [HideInInspector] public Inventory inventoryComponent;
    [HideInInspector] public GameObject spellReadyEffect;

    public GameObject harmEffect;


    //[HideInInspector] public Vector3 effectAnchorPos;

    private void Awake()
    {
        enemy = gameObject.GetComponent<Enemy>();

        effectAnchorPos = transform.Find("EffectAnchor").transform.position;

        startingPos = new Vector2(transform.position.x, transform.position.y);

        inventoryComponent = GameObject.Find("Player").GetComponent<Inventory>();

        //Have to do it this way since the object is inactive
        SpellReadyEffect[] spellReadyComp = Resources.FindObjectsOfTypeAll<SpellReadyEffect>();

        spellReadyEffect = spellReadyComp[0].gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = enemy.animator;
        moveSpeed = enemy.speed;


        target = SelectCharacter();

        Debug.Log("target:" + target.name);

        Debug.Log(enemy.enemyName);

        idleState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);

        //No matter what state the enemy is in
        if (enemy.healthComponent.GetHealth() <= 0)
        {
            if (currentState != deadState)
            {
                deadState.EnterState(this);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (inventoryComponent.hasPreparedSpell())
            {
                if (inventoryComponent.preparedSpell[0] == "Rune_ TargetEnemies")
                {
                    RecieveSpellEffects(inventoryComponent.preparedSpell);
                }
            }
        }
    }

    public bool EnemyIsWithinRange()
    {
        if (target != null)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.transform.position);

            bool isWithinRange = distanceToTarget >= enemy.minDistance && distanceToTarget <= enemy.maxDistance;

            return isWithinRange;
        }
        else
        {
            return true;
        }

    }

    public void FlipSprite(string dir)
    {
        float dirNumber = 1f;
        if (dir == "left") dirNumber = -1f;
        else if (dir == "right") dirNumber = 1f;

        if (enemy.sprite != null)
        {
            enemy.sprite.transform.localScale = new Vector3(dirNumber, enemy.sprite.transform.localScale.y, enemy.sprite.transform.localScale.z);
        }
        else
        {
            //super temporary//
            /*transform.localScale = new Vector3(dirNumber, transform.localScale.y, transform.localScale.z);
            var canvasScale = transform.Find("CharacterCanvas").localScale;
            transform.Find("CharacterCanvas").localScale = new Vector3(dirNumber / 1000f, canvasScale.y, canvasScale.z);*/
        }
    }

    public GameObject SelectCharacter()
    {
        GameObject target;
        List<GameObject> aliveCharacters = FindAliveCharacters();

        if (aliveCharacters.Count > 1)
        {
            List<GameObject> closestAliveCharacters = aliveCharacters.OrderBy(x => Vector2.Distance(this.transform.position, x.transform.position)).ToList();

            target = closestAliveCharacters[0];

            /*string characterName = randomCharacter.GetComponent<Character>().characterName;
            if (characterName == "Artillerist" && enemy.enemyName != "XbowRaider")
            {
                while (randomCharacter.GetComponent<Character>().characterName == "Artillerist")
                {
                    randomCharacter = aliveCharacters[HelperUtilities.RandInt(0f, aliveCharacters.Count - 1)];
                }
                target = randomCharacter;
            }
            else
            {
                target = aliveCharacters[HelperUtilities.RandInt(0f, aliveCharacters.Count - 1)];
            }*/
        }
        else if (aliveCharacters.Count == 1)
        {
            target = aliveCharacters[0];
        }
        else
        {
            target = GameObject.Find("Player");
        }

        return target;
    }

    public List<GameObject> FindAliveCharacters()
    {
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");

        List<GameObject> aliveCharacters = new List<GameObject>();

        foreach (GameObject character in characters)
        {
            CharacterStateManager characterSM = character.GetComponent<CharacterStateManager>();
            if (characterSM.currentState != characterSM.deadState)
            {
                if (!aliveCharacters.Contains(character))
                {
                    aliveCharacters.Add(character);
                }
            }
        }

       /* GameObject player = GameObject.Find("Player");

        if (player.GetComponent<Health>().GetHealth() > 0) aliveCharacters.Add(player);*/

        return aliveCharacters;
    }

    public bool PlayerIsAlive()
    {
        GameObject player = GameObject.Find("Player");
        int playerHealth = player.GetComponent<Health>().GetHealth();

        return playerHealth > 0;
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

        foreach (string rune in _spell)
        {
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

        if (harmCount > 0) 
        {
            enemy.TakeDamage(harmAmt * harmCount);
            InstantiateEffectPrefab(harmEffect);
            ScreenShake.Instance.ShakeCamera(5, 0.1f, true);
        } 

        StartCoroutine(TimedSpellReset());

    }

    IEnumerator TimedSpellReset()
    {
        yield return new WaitForSeconds(0.2f);

        inventoryComponent.UsePreparedSpell();
        spellReadyEffect.SetActive(false);
    }
}
