using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponGenre
{
    MELEE,
    RANGE,
    MAGIC
}

[CreateAssetMenu]
public class WeaponSO : ItemSO
{
    public List<WeaponGenre> weaponGenres;
    public BulletSO bullet;
    public float attackRatePerSecond;
    public float projectileSpeed;
    public float projectilAmountPerShooting;
    public float cooldownToNextAttack;
}
