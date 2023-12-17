using Assets.Scripts.Data_stucture.Skill;
using Assets.Scripts.Helper;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.XR;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ActiveSkillSO")]
public class ActiveSkillSO : ItemSO
{
    public Skill skill;
    [Header("Stats")]
    public List<StatType> statToApply;
    public List<SkillType> skillType;
    public float valueToApply;
    public float probability;

    public bool readyToShoot { get; set; } = true;
    public Vector3 mousePosition;
    public bool HasData()
    {
        // Check if statToApply is not null or empty, and valueToApply is not zero
        return statToApply != null && statToApply.Count > 0 && valueToApply != 0f;
    }
    public void ApplyStat()
    {
        Player player = PlayerManager.Instance.player;
        statToApply.ForEach(stat => {
            switch (stat)
            {
                case StatType.Hp:
                    player.hp.AddModifier(valueToApply);
                    break;
                case StatType.Mp:
                    player.mp.AddModifier(valueToApply);
                    break;
                case StatType.Defense:
                    player.defense.AddModifier(valueToApply);
                    break;
                case StatType.Strength:
                    player.strength.AddModifier(valueToApply);
                    break;
                case StatType.Speed:
                    player.speed.AddModifier(valueToApply);
                    break;
                case StatType.AttackRatePerSecond:
                    player.attackRatePerSecond.AddModifier(valueToApply);
                    break;
                case StatType.ProjectileSpeed:
                    player.projectileSpeed.AddModifier(valueToApply);
                    break;
                case StatType.ProjectileAmount:
                    player.projectileAmount.AddModifier(valueToApply);
                    break;
                case StatType.CooldownToNextAttack:
                    player.cooldownToNextAttack.AddModifier(valueToApply);
                    break;
                default:
                    break;
            }
        });
    }

    public string GetStatName(StatType statType)
    {
        switch (statType)
        {
            case StatType.Hp:
                return "Hp";
            case StatType.Mp:
                return "Mp";
            case StatType.Defense:
                return "Def";
            case StatType.Strength:
                return "Strength";
            case StatType.Speed:
                return "Movement Speed";
            case StatType.AttackRatePerSecond:
                return "Attack Rate Per Second";
            case StatType.ProjectileSpeed:
                return "Projectile Speed";
            case StatType.ProjectileAmount:
                return "Projectile Amount";
            case StatType.CooldownToNextAttack:
                return "Cooldown to next attack";
            default:
                return "";
        }
    }
    public List<StatType> GetStatType() => statToApply;

    #region Logic Of Skill

    public void ActivateSkill(MonoBehaviour coroutineExecutor) 
    {
        skill.Activate(coroutineExecutor);
    }

    #endregion
}
