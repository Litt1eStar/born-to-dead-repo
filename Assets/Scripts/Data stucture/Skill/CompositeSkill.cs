using Assets.Scripts.Helper;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Data_stucture.Skill
{
    
    public class CompositeSkill : ActiveSkillSO
    {
        [SerializeField] private ActiveSkillSO f_skill;//first skill
        [SerializeField] private ActiveSkillSO s_skill;//second skill

    }
}