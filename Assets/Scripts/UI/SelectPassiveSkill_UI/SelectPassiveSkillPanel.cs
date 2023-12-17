using Assets.Scripts.Helper;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class SelectPassiveSkillPanel : MonoBehaviour
{
    public int amountOfSkillInPanel;

    public int rerollAmount = 3;

    public Transform content;
    public GameObject passiveSkillObj;
    public GameObject activeSkillObj;

    public GameObject switchToActiveSkillBtn;
    public GameObject switchToPassiveSkillBtn;
    public void AddPassiveSkillToPanel()
    {
        ClearItemInPanel();

        for (int i = 0; i < amountOfSkillInPanel; i++) 
        {
            GameObject itemOnPanel = Instantiate(passiveSkillObj, content);
            PassiveSkill passiveSkillComponent = itemOnPanel.GetComponent<PassiveSkill>();
            passiveSkillComponent.SetupItemData();
        }
    }

    public void AddActiveSkillToPanel()
    {
        ClearItemInPanel();

        for (int i = 0; i < amountOfSkillInPanel; i++)
        {
            GameObject itemOnPanel = Instantiate(activeSkillObj, content);
            ActiveSkill activeSkillComponent = itemOnPanel.GetComponent<ActiveSkill>();
            activeSkillComponent.SetupItemData();
        }
    }

    public void ClearItemInPanel()
    {
        foreach (Transform item in content.transform)
        {
            Destroy(item.gameObject);
        }
    }

    public void Reroll()
    {
        if (rerollAmount <= 0)
            return;

        rerollAmount--;
        DebugHelper.Debugger(this.name, $"Reroll Amount Left {rerollAmount}");

        foreach (Transform item in content)
        {
            if (item.gameObject.GetComponent<ActiveSkill>())
            {
                AddActiveSkillToPanel();
                return;
            }
            else if (item.gameObject.GetComponent<PassiveSkill>())
            {
                AddPassiveSkillToPanel();
                return;
            }

        }
    }

    public void SwitchToActiveSkillPanel()
    {
        switchToActiveSkillBtn.SetActive(false);
        switchToPassiveSkillBtn.SetActive(true);

        AddActiveSkillToPanel();
    }

    public void SwitchToPassiveSkillPanel()
    {
        switchToPassiveSkillBtn.SetActive(false);
        switchToActiveSkillBtn.SetActive(true);

        AddPassiveSkillToPanel();
    }
}
