using Assets.Scripts.Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private List<ActiveSkillSO> activeSkillItemInInventory;
    private void Update()
    {
        activeSkillItemInInventory = PlayerManager.Instance.inventory.activeSkillInventory.GetActiveSkillInInventory();

        activeSkillItemInInventory.ForEach(skill => {
            //DebugHelper.Debugger(this.name, $"skill to Execute : {skill.Name}");
            skill.ActivateSkill(this);
        });

         
    }
}
