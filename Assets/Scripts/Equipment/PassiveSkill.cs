using Assets.Scripts.Helper;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PassiveSkill : MonoBehaviour
{
    [Header("PassiveSkill")]
    public List<PassiveSkillSO> listOfPassiveSkill;
    public PassiveSkillSO passiveSkill;
    
    public Image skillIcon;
    public TextMeshProUGUI skillName;
    public TextMeshProUGUI skillDesc;

    private int constantAmountToAdd = 1;
    public void SetupItemUI()
    {
        skillIcon.sprite = passiveSkill.sprite;
        skillName.text = passiveSkill.Name;

        passiveSkill.statToApply.ForEach(st => {
            skillDesc.text = passiveSkill.GetStatName(st) + $" + {Mathf.FloorToInt(passiveSkill.valueToApply)}";
        });
    }

    [System.Obsolete]
    private void Update()
    {
        if (gameObject.active)
        {
            if(passiveSkill != null)
                SetupItemUI();
        }
    }

    public void SetupItemData()
    {
        if (passiveSkill != null && passiveSkill.HasData())
        {
            DebugHelper.Debugger(this.name, "Can't Setup Item Data");
            return;
        }

        int selectedSkillIndex = -1;

        // Use LINQ to filter skills with data
        var skillsWithData = listOfPassiveSkill.Where(skill => skill.HasData()).ToList();

        if (skillsWithData.Count > 0)
        {
            selectedSkillIndex = Random.Range(0, skillsWithData.Count);
            passiveSkill = skillsWithData[selectedSkillIndex];
            DebugHelper.Debugger(this.name, $"Selected Skill Index: {selectedSkillIndex} | Item Name: {passiveSkill.Name}");
        }
    }
    public void OnItemClicked()
    {
        if (passiveSkill != null)
        {
            PlayerManager.Instance.inventory.passiveSkillInventory.AddItem(passiveSkill as ItemSO, constantAmountToAdd);
            passiveSkill.ApplyStat();
        }

        Destroy(this.gameObject);
        GameplayManager.Instance.UpdateGameState(GameState.OnGameplay);
        UIManager.Instance.selectSkillPanel.SetActive(false);
        UIManager.Instance.profilePanel.SetActive(false);
    }
}
