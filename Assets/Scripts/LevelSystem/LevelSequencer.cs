using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSequencer : MonoBehaviour
{

    public static LevelSequencer Instance { get; private set; }

    public GameObject enemyPrefab;
    public List<EnemyDetailsSO> enemies;
    [HideInInspector] public List<GameObject> currentEnemies;
    bool currentEnemyWaveDead;

    public List<GameObject> enemySpawnPoints = new List<GameObject>();

    private void Awake()
    {
        StartCoroutine(Sequence());
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Sequence()
    {
        SpawnEnemies(5);

        //yield return new WaitUntil(() => !currentEnemyWaveDead);

        //while (!currentEnemyWaveDead) { };

        //yield return new WaitForSeconds(60);

        //SpawnEnemies(5);

        //yield return new WaitUntil(() => !currentEnemyWaveDead);

        //while (!currentEnemyWaveDead) { };

        yield break;
    }

    private void SpawnEnemies(int _amt)
    {
        //reset bool 
        currentEnemyWaveDead = false;

        int enemyIndex = 0;

        for (int i=0; i<_amt; i++)
        {
            int randIndex = HelperUtilities.RandInt(0f, enemySpawnPoints.Count);

            Vector3 selectedSpawn = enemySpawnPoints[i].transform.position;

            //int randIndexForPrefabs = HelperUtilities.RandInt(0f, enemies.Count-1);

            enemyIndex += 1;

            if (enemyIndex > 2) enemyIndex -= 3;

            EnemyDetailsSO selectedEnemy = enemies[enemyIndex];

            GameObject newEnemy = Instantiate(selectedEnemy.enemyPrefab);

            newEnemy.GetComponent<Enemy>().Initialize(selectedEnemy);

            newEnemy.transform.position = new Vector3(selectedSpawn.x, selectedSpawn.y, newEnemy.transform.position.z);

            currentEnemies.Add(newEnemy);
        }
    }

    public void UpdateEnemyList(GameObject enemy)
    {
        currentEnemies.Remove(enemy);
        if(currentEnemies.Count == 0)
        {
            currentEnemyWaveDead = true;
        }
    }


}
