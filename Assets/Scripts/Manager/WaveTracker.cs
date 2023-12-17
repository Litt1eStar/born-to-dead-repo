using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTracker : MonoBehaviour
{
    public static WaveTracker Instance;

    public int enemySpawnCount { get; set; }
    public int enemyDeadCount { get; set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }
    public void IncreseEnemySpawnCount()
    {
        enemySpawnCount++;
    }

    public void IncreseEnemyDeadCount()
    {
        enemyDeadCount++;
    }

    public List<int> GetWaveInfo()
    {
        return new List<int> { enemySpawnCount, enemyDeadCount};
    }
}
