using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(menuName = "CharacterClass")]
public class CharacterClassSO : ScriptableObject
{
    [Header("Class Info")]
    public ActiveSkillSO uniqueSkill;
    public string className;
    public Sprite sprite;
    public RuntimeAnimatorController ac;

    [Header("Class Base Stat")]
    public Stat hp;
    public Stat mp;
    public Stat defense;
    public Stat strength;
    public Stat speed;
    public Stat attackRatePerSecond;
    public Stat projectileSpeed;
    public Stat projectileAmount;
    public Stat cooldownToNextAttack;
}
