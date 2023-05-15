using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelSequencer : MonoBehaviour
{

    public static LevelSequencer Instance { get; private set; }

    public GameObject enemyPrefab;
    public List<EnemyDetailsSO> enemies;
    [HideInInspector] public List<GameObject> currentEnemies;
    bool currentEnemyWaveDead;

    public List<GameObject> enemySpawnPoints = new List<GameObject>();

    private Inventory inventoryComponent;
    private InkCounter inkCounter;

    //Levels
    Phase[] level1 = new Phase[] { new Phase(PhaseType.Camping, 2), /*new Phase(PhaseType.ResourceDrop, 12),*/ new Phase(PhaseType.EnemySpawn, 3), 
        new Phase(PhaseType.Camping, 30), new Phase(PhaseType.EnemySpawn, 5), new Phase(PhaseType.ResourceDrop, 12),
        new Phase(PhaseType.Camping, 45), new Phase(PhaseType.EnemySpawn, 6)
    };

    int phaseCounter;
    Phase currentPhase;
    bool phaseCondition;
    float phaseTimer;
    float phaseTimerSet;
    int enemyCount;
    bool switchingPhase;

    //UI//
    public TextMeshProUGUI phaseText;


    private void Awake()
    {
        inventoryComponent = GameObject.Find("Player").GetComponent<Inventory>();
        inkCounter = GameObject.Find("InkCounter").GetComponent<InkCounter>();
    }

    private void OnEnable()
    {
        StaticEventHandler.EnemyDiedEvent += UpdateEnemyCount;
    }

    private void OnDisable()
    {
        StaticEventHandler.EnemyDiedEvent -= UpdateEnemyCount;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyCount = 0;
        phaseCounter = 0;
        phaseCondition = false;
        StartCoroutine(Sequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Sequence()
    {
        while (phaseCounter != level1.Length)
        {
            currentPhase = level1[phaseCounter];

            Debug.Log("Phase " + phaseCounter + " Started");

            InitializePhase(currentPhase.phaseType, currentPhase.phaseData);

            Debug.Log(currentPhase.phaseType);

            //When the phase condition is met, we will increment the counter and start the next wave
            while (!phaseCondition)
            {
                //We do these things until the phase condition is met
                if (currentPhase.phaseType == PhaseType.EnemySpawn)
                {
                    phaseText.text = "Enemies Incoming!";

                    if (enemyCount <= 0)
                    {
                        Debug.Log(enemyCount);
                        phaseCondition = true;
                    }
                }
                else if (currentPhase.phaseType == PhaseType.Camping)
                {
                    if (phaseTimer > 0)
                    {
                        phaseTimer -= Time.deltaTime;
                        phaseText.text = "Camping Time: " + Mathf.Ceil(phaseTimer).ToString();
                    }
                    else
                    {
                        phaseCondition = true;
                    }
                }
                else if (currentPhase.phaseType == PhaseType.ResourceDrop)
                {
                    //Drop resources
                    inventoryComponent.AddMoreCraftingIngredients((int)currentPhase.phaseData);
                    inkCounter.AddInk(HelperUtilities.RandInt(2f, 7f));
                    StaticEventHandler.CallResourceDropEvent();
                    phaseCondition = true;
                }

                yield return null;
            };

            phaseCounter += 1;

            Debug.Log("About To Start Phase " + phaseCounter);

            yield return null;
        }

        phaseText.text = "Victory!";
    }

    private void SpawnEnemies(float _amt)
    {
        //reset bool 
        currentEnemyWaveDead = false;

        int enemyIndex = 0;

        for (int i = 0; i < _amt; i++)
        {
            Debug.Log("Spawning Enemy");
            int randIndex = HelperUtilities.RandInt(0f, enemySpawnPoints.Count);

            Vector3 selectedSpawn = enemySpawnPoints[i].transform.position;

            //int randIndexForPrefabs = HelperUtilities.RandInt(0f, enemies.Count-1);

            enemyIndex += 1;

            if (enemyIndex > 2) enemyIndex -= 3;

            EnemyDetailsSO selectedEnemy = enemies[enemyIndex];

            GameObject newEnemy = Instantiate(selectedEnemy.enemyPrefab);

            newEnemy.GetComponent<Enemy>().Initialize(selectedEnemy);

            newEnemy.transform.position = new Vector3(selectedSpawn.x, selectedSpawn.y, newEnemy.transform.position.z);

            //currentEnemies.Add(newEnemy);
        }
        StaticEventHandler.CallEnemySpawnedEvent();
        switchingPhase = false;
    }

    public void UpdateEnemyCount(EnemyDiedEventArgs eventArgs)
    {
        GameObject[] remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = remainingEnemies.Length - 1;
        Debug.Log("EnemyCount: " + enemyCount);
    }

    public void InitializePhase(PhaseType _phaseType, float _phaseData)
    {
        phaseCondition = false;
        if (_phaseType == PhaseType.EnemySpawn)
        {
            SpawnEnemies(currentPhase.phaseData);
            enemyCount = (int) currentPhase.phaseData;
        }

        if (_phaseType == PhaseType.Camping)
        {
            SetPhaseTimer(currentPhase.phaseData);
        }
    }

    public void SetPhaseTimer(float _timerSet)
    {
        phaseTimerSet = _timerSet;
        phaseTimer = phaseTimerSet;
        switchingPhase = false;
    }

    public enum PhaseType
    {
        EnemySpawn,
        Camping,
        ResourceDrop,
    }

    public class Phase
    {
        public Phase(PhaseType _phaseType, float _phaseData) 
        {
            phaseType = _phaseType;
            phaseData = _phaseData;
        }
        public PhaseType phaseType;
        public float phaseData;
    }
}
