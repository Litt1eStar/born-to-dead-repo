using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Keycode")]
    public KeyCode openStatKC = KeyCode.F2;
    public KeyCode testKC = KeyCode.F1;

    [Header("Level UI")]
    public Image levelBar;
    public Image hpBar;
    public TextMeshProUGUI levelTxt;

    [Header("Wave UI")]
    public TextMeshProUGUI waveIndicator;
    public TextMeshProUGUI waveStatus;
    public TextMeshProUGUI enemySpawnStatTxt;
    public TextMeshProUGUI enemyDeadStatTxt;

    [Header("Player Profile UI")]
    public UIInvetoryPage invPage;
    public PlayerProfileUI playerProfile;
    public GameObject profilePanel;
    public TextMeshProUGUI profileStatTxt;

    [Header("PlayerStats UI")]
    public GameObject statPanel;
    public TextMeshProUGUI statTxt;
    public TextMeshProUGUI hpTxt;
    public TextMeshProUGUI mpTxt;
    public TextMeshProUGUI defTxt;
    public TextMeshProUGUI strTxt;
    public TextMeshProUGUI speedTxt;
    public TextMeshProUGUI attackRateTxt;
    public TextMeshProUGUI projectileSpeedTxt;
    public TextMeshProUGUI projectileAmountTxt;

    private bool statUIOpening;

    [Header("Select PassiveSkill UI")]
    public SelectPassiveSkillPanel SelectPassiveSkill;
    public GameObject selectSkillPanel;

    private Player playerStat;

    private void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(Instance.gameObject);
        else
            Instance = this;        
    }

    private void Start()
    {
        playerStat = PlayerManager.Instance.player;
        UpdateLevelText(1);
        ClearStatUI();
        statUIOpening = false;
        selectSkillPanel.SetActive(false);
        profilePanel.SetActive(false);
    }

    private void Update()
    {
        UpdateWaveTrackerUI();
        StatUIHandler();
    }

    public void OpenSkillSelectionUIPanel()
    {
        profilePanel.SetActive(true);
        selectSkillPanel.SetActive(true);
        SelectPassiveSkill.AddPassiveSkillToPanel();
    }
    public void StatUIHandler()
    {
        hpTxt.text = GetFormattedText("HP", playerStat.hp.GetValue(), GetColor(playerStat.hp.GetValue()), "");
        mpTxt.text = GetFormattedText("MP", playerStat.mp.GetValue(), GetColor(playerStat.mp.GetValue()), "");
        defTxt.text = GetFormattedText("Defense", playerStat.defense.GetValue(), GetColor(playerStat.defense.GetValue()), "");
        strTxt.text = GetFormattedText("Strenght", playerStat.strength.GetValue(), GetColor(playerStat.strength.GetValue()), "");
        speedTxt.text = GetFormattedText("Speed", playerStat.speed.GetValue(), GetColor(playerStat.speed.GetValue()), "");
        attackRateTxt.text = GetFormattedText("Attack Rate", playerStat.attackRatePerSecond.GetValue(), GetColor(playerStat.attackRatePerSecond.GetValue()), "");
        projectileSpeedTxt.text = GetFormattedText("Projectile Speed", playerStat.projectileSpeed.GetValue(), GetColor(playerStat.projectileSpeed.GetValue()), "");
        projectileAmountTxt.text = GetFormattedText("Projectile Amount", playerStat.projectileAmount.GetValue(), GetColor(playerStat.projectileAmount.GetValue()), "");
    }

    public string GetFormattedText(string type,float value, string color, string additionalString)
    {
        return $"{type} : <color={color}>{value}</color> {additionalString}";
    }
    public void ClearStatUI()
    {
        statPanel.SetActive(false);
        statTxt.text = "";
    }
    public void UpdateLevelText(int currentLevel)
    {
        levelTxt.text = "Level : " + currentLevel.ToString();
        Debug.Log("Current Level : " + currentLevel);
    }
    public void UpdateLevelBarUI(float currentExp, float totalExp)
    {
        float fillAmount = Mathf.Clamp01(currentExp / totalExp);
        levelBar.fillAmount = fillAmount;
    }
    public void UpdateHealthBarUI(float currentHp, float maxHp)
    {
        float fillAmount = Mathf.Clamp01(currentHp / maxHp);
        hpBar.fillAmount = fillAmount;
    }
    public void ClearHpUI()
    {
        hpBar.fillAmount = 0;
    }
    public string GetColor(float value)
    {
        // Add your color conditions here
        if (value > 50)
            return "green";
        else if (value > 25)
            return "yellow";
        else
            return "red";
    }
    #region Wave UI
    public void UpdateWaveTrackerUI()
    {
        enemySpawnStatTxt.text = $"Enemy Spawn = {WaveTracker.Instance.enemySpawnCount}";
        enemyDeadStatTxt.text = $"Enemy Dead = {WaveTracker.Instance.enemyDeadCount}";
    }
    public void UpdateWaveIndicator(float wave)
    {
        waveIndicator.text = "Wave " + Mathf.FloorToInt(wave).ToString();
    }
    public void UpdateWaveState(WaveSpawner.SpawnState waveState)
    {
        waveStatus.text = waveStatus.ToString();
    }
    #endregion

}
