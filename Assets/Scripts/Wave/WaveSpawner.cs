
using Assets.Scripts.Helper;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public List<EnemySO> enemy;
        public int count;
        public float rate;
    }

    [Header("Entity")]
    public List<EnemySO> listOfEnemyToSpawn;

    [Header("Wave Setting")]
    public Wave[] waves;
    public Transform[] spawnPoints;
    public float timeBetweenWaves = 5f;

    int currentWaveIndex = 0; //start at first position of array
    int enemyAmount = 2; //start at 1 type of enemy

    float waveCountDown;
    float searchCountDown = 1f;

    SpawnState state = SpawnState.COUNTING;

    void Start()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("You forgot to put in spawnpoint, idiots");
        }
    }

    void Update()
    {

        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountDown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[currentWaveIndex]));
                UIManager.Instance.UpdateWaveIndicator(currentWaveIndex + 1);
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed");

        state = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;

        if (currentWaveIndex + 1 > waves.Length - 1)
        {
            // Implemnt some sorta multiplier here to make it harder over time
            currentWaveIndex = 0;
            Debug.Log("ALL WAVES COMPLETE! Looping...");
        }
        else
        {
            int previousWaveIndex = currentWaveIndex;
            currentWaveIndex++;
            waves[currentWaveIndex].enemy = UpdateNextWaveData();

            float waveCoefficient = currentWaveIndex == 1 ? 2f : Mathf.Pow(currentWaveIndex, 2);
            waves[currentWaveIndex].count = Mathf.RoundToInt(waves[previousWaveIndex].count * waveCoefficient);

            waves[currentWaveIndex].rate = waves[previousWaveIndex].rate + 0.15f;
            DebugHelper.Debugger(this.name, $"Enemy in Wave Count = {waves[currentWaveIndex].enemy.Count}");
        }
    }

    private List<EnemySO> UpdateNextWaveData()
    {
        List<EnemySO> enemyData = new List<EnemySO>();

        for (int i = 0; i < listOfEnemyToSpawn.Count; i++)
        {
            float rand = Mathf.FloorToInt(Random.Range(0f, 100f));
            for (int j = 0; j < listOfEnemyToSpawn.Count; j++)
            {
                if (enemyData.Count < enemyAmount)
                {
                    DebugHelper.Debugger(this.name, "Add Enemy in Process");

                    if (rand <= listOfEnemyToSpawn[j].GetSpawnChanceBaseOnRarity())
                    {
                        DebugHelper.Debugger(this.name, $"Check Enemy name : {listOfEnemyToSpawn[j].name}");
                        enemyData.Add(listOfEnemyToSpawn[j]);
                    }

                }
            }
        }

        if (enemyData.Count <= 0)
        {
            DebugHelper.Debugger(this.name, "There is no enemy in the next Wave");
        }

        return enemyData;
    }

    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if (searchCountDown <= 0)
        {
            searchCountDown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawn Wave:" + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            //Fix it to be generated based on tier of Enemy
            for (int j = 0; j < _wave.enemy.Count; j++)
            {
                if (Random.Range(1f, 101f) <= _wave.enemy[j].spawnChance)
                    SpawnEnemy(_wave.enemy[j].prefab);
            }
            //Fix it to be generated based on tier of Enemy
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(GameObject _enemy)
    {
        Debug.Log("Spawning Enemy: " + _enemy.name);

        WaveTracker.Instance.IncreseEnemySpawnCount();

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
    }
}