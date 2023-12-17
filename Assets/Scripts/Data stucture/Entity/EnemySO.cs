using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{
    Boss,
    MiniBoss,
    Minion
}

[CreateAssetMenu(menuName = "EnemySO")]
public class EnemySO : ScriptableObject
{
    public float attackRange;
    public int expAmount = 100;
    public float spawnChance;
    public GameObject prefab;

    public Rarity rarity;

    public int GetSpawnChanceBaseOnRarity()
    {
        switch (rarity)
        {
            case Rarity.Boss:
                return 10;
            case Rarity.MiniBoss:
                return 30;
            case Rarity.Minion:
                return 80;
            default:
                return 0;
        }
    }
}
