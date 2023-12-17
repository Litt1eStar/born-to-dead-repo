using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterStats : MonoBehaviour
{
    public float currentHealth;

    public Stat hp;
    public Stat mp;
    public Stat defense;
    public Stat strength;
    public Stat speed;
    public Stat attackRatePerSecond;
    public Stat projectileSpeed;
    public Stat projectileAmount;
    public Stat cooldownToNextAttack;

    public float currentExp, maxExp;
    public float currentLevel;

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer sr { get; private set; }
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }
    protected virtual void Awake()
    {
        currentHealth = hp.GetValue();
        currentLevel = 1;
    }
    protected virtual void Update()
    {
    }

    public void HandleExperienceChange(int newExp)
    {
        currentExp += newExp;
        if (currentExp >= maxExp)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        hp.AddModifier(10);
        currentHealth = hp.GetValue();
        currentLevel++;

        currentExp = 0;
        maxExp += 100;

        UIManager.Instance.UpdateLevelText(Mathf.FloorToInt(currentLevel));
        UIManager.Instance.OpenSkillSelectionUIPanel();
        GameplayManager.Instance.UpdateGameState(GameState.OnSelectionUI);
    }

    public virtual void TakeDamage(float _damage)
    {
        _damage -= defense.GetValue();
        _damage = Mathf.Clamp(_damage, 0, int.MaxValue);
        currentHealth -= _damage;
        StartCoroutine(ColorBlinkFX(Color.red, .3f));
        if (_damage > currentHealth)
        {
            currentHealth = 0;
        }
        else
        {
            currentHealth -= _damage;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public IEnumerator ColorBlinkFX(Color blink_c, float blink_d)
    {
        Color defaultColor = sr.color;
        float elapsedTime = 0f;

        while (elapsedTime < blink_d)
        {
            sr.color = Color.Lerp(defaultColor, blink_c, Mathf.PingPong(elapsedTime, 0.5f));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        sr.color = defaultColor;
    }
    protected virtual void Die()
    {
        Debug.Log("Die");
        UIManager.Instance.ClearHpUI();
        SceneManager.LoadScene("LoseMenu");
    }

}