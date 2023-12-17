using Assets.Scripts.Helper;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActiveSkill : MonoBehaviour
{
    [Header("PassiveSkill")]
    public List<ActiveSkillSO> listOfActiveSkill;
    public ActiveSkillSO activeSkill;

    public Image skillIcon;
    public TextMeshProUGUI skillName;
    public TextMeshProUGUI skillDesc;

    private int constantAmountToAdd = 1;
    private Player playerStat;
    public void SetupItemUI()
    {
        skillIcon.sprite = activeSkill.sprite;
        skillName.text = activeSkill.Name;

        activeSkill.statToApply.ForEach(st => { 
            skillDesc.text = activeSkill.GetStatName(st) + $" + {Mathf.FloorToInt(activeSkill.valueToApply)}";
        });
    }
    private void Start()
    {
        playerStat = PlayerManager.Instance.player;
    }
    [System.Obsolete]
    private void Update()
    {
        if (gameObject.active)
        {
            if (activeSkill != null)
                SetupItemUI();
        }
    }
    public void SetupItemData()
    {
        if (activeSkill != null && activeSkill.HasData())
        {
            DebugHelper.Debugger(this.name, "Can't Setup Item Data");
            return;
        }

        int selectedSkillIndex = -1;

        // Use LINQ to filter skills with data
        var skillsWithData = listOfActiveSkill.Where(skill => skill.HasData()).ToList();

        if (skillsWithData.Count > 0)
        {
            selectedSkillIndex = Random.Range(0, skillsWithData.Count);
            activeSkill = skillsWithData[selectedSkillIndex];
            DebugHelper.Debugger(this.name, $"Selected Skill Index: {selectedSkillIndex} | Item Name: {activeSkill.Name}");
        }
    }
    public void OnItemClicked()
    {
        if (activeSkill != null)
        {
            PlayerManager.Instance.inventory.activeSkillInventory.AddItem(activeSkill as ItemSO, constantAmountToAdd);
            activeSkill.ApplyStat();
        }

        Destroy(this.gameObject);
        GameplayManager.Instance.UpdateGameState(GameState.OnGameplay);
        UIManager.Instance.selectSkillPanel.SetActive(false);
        UIManager.Instance.profilePanel.SetActive(false);
    }
}
