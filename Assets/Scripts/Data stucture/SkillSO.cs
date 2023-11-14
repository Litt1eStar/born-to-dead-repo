using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType
{
    ActiveSkill,
    PassiveSkill
}

public enum SkillGenre
{
    AOE,
    SINGLE,
    BURST
}

public enum Elemetal
{
    Fire,
    Ice,
    Dark
}

[CreateAssetMenu]
public class SkillSO : ItemSO
{
    public SkillType skillType;
    public List<SkillGenre> skillGenres;
    public List<AbilitySO> skillAbilitys;
    public List<Elemetal> skillElementals;
    public List<Stat> statsToApply;
    public float damagePerHit;
    public float skillCooldown;
}
