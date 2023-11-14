using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Sword,
    Bow,
    Staff
}

[CreateAssetMenu(fileName = "Weapon Base Class")]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public string description;
    public WeaponType type;

    public float attackRatePerSecond;
    public float projectileSpeed;
    public float projectileAmount;
    public float cooldownToNextAttack;

    public List<object> weaponAbility;
    public List<object> weaponElemental;

    public GameObject prefab;
}
