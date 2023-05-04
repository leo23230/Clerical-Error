using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [HideInInspector] public Vector2 startingPos = new Vector2();

    //[HideInInspector] public Vector3 effectAnchorPos;

    private void Awake()
    {
        enemy = gameObject.GetComponent<Enemy>();

        startingPos = new Vector2(transform.position.x, transform.position.y);
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
            GameObject randomCharacter = aliveCharacters[HelperUtilities.RandInt(0f, aliveCharacters.Count - 1)];

            string characterName = randomCharacter.GetComponent<Character>().characterName;
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
            }
        }
        else if (aliveCharacters.Count == 1)
        {
            target = aliveCharacters[0];
        }
        else
        {
            target = null;

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

        return aliveCharacters;
    }
}
