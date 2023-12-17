using Assets.Scripts.Helper;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Entity
{
    [Header("Data")]
    public WeaponSO weapon;
    public CharacterClassSO c_class;
    public Transform posToSpawn;

    private Vector3 mousePosition;
    private bool flag = true;

    #region Movement
    [Header("Movement Settings")]
    public float dashForce;
    public float dashCooldown;
    public int facingDir { get; private set; }
    public bool facingRight { get; private set; }
    public float dashDir { get; private set; }
    #endregion
    #region References
    [Header("System")]
    public SpriteRenderer w_sr;
    public List<ActiveSkillSO> activeSkillItemInInventory;
    #endregion
    #region Input Key
    public KeyCode dashKey = KeyCode.LeftShift;
    #endregion
    #region State
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Moving");
        dashState = new PlayerDashState(this, stateMachine, "Moving");

    }
    protected override void Start()
    {
        base.Start();

        SetupCharacterClass();
        SetupWeapon();

        
        activeSkillItemInInventory = new()
        {
            c_class.uniqueSkill
        };

        PlayerManager.Instance.inventory.activeSkillInventory.AddItem(c_class.uniqueSkill as ItemSO, 1);
        activeSkillItemInInventory[0].readyToShoot = true;
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        AimHandler();

        activeSkillItemInInventory = PlayerManager.Instance.inventory.activeSkillInventory.GetActiveSkillInInventory();


        stateMachine.currentState.Update();

        FacingDirection();
        DashInputHandler();

        if (Input.GetKeyDown(KeyCode.F10))
        {
            UIManager.Instance.profilePanel.SetActive(flag);
            flag = !flag;
        }

        UIManager.Instance.UpdateLevelBarUI(currentExp, maxExp);
        UIManager.Instance.UpdateHealthBarUI(currentHealth, hp.GetValue());
    }
    private void AimHandler()
    {
        GameObject aimTransform = GameObject.FindGameObjectWithTag("Aim");
        mousePosition = InputManager.Instance.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.transform.eulerAngles = new Vector3(0, 0, angle);
    }
    
    public void UpdateCurrentInventoryData()
    {
        activeSkillItemInInventory = PlayerManager.Instance.inventory.activeSkillInventory.GetActiveSkillInInventory();
    }

    private void SetupCharacterClass()
    {
        string selectedClassName = PlayerPrefs.GetString("SelectedClass", "Default");
        CharacterClassSO selectedClass = ClassList.FindClassByName(selectedClassName);

        if (selectedClass != null)
        {
            c_class = selectedClass;
            UIManager.Instance.invPage.UpdateActiveSkillInventoryData(0, c_class.uniqueSkill.sprite, 1);
            SetupPlayerSpriteInfo();
            SetupCharacterStats();
        }
    }

    private void SetupCharacterStats()
    {
        hp.AddModifier(c_class.hp.GetValue());
        mp.AddModifier(c_class.mp.GetValue());
        defense.AddModifier(c_class.defense.GetValue());
        strength.AddModifier(c_class.strength.GetValue());
        speed.AddModifier(c_class.speed.GetValue());
        attackRatePerSecond.AddModifier(c_class.attackRatePerSecond.GetValue());
        projectileSpeed.AddModifier(c_class.projectileSpeed.GetValue());
        projectileAmount.AddModifier(c_class.projectileAmount.GetValue());
        cooldownToNextAttack.AddModifier(c_class.cooldownToNextAttack.GetValue());

        currentHealth = hp.GetValue();
        DebugHelper.Debugger(this.name, "SetupCharacrterStats");
    }

    private void SetupPlayerSpriteInfo()
    {
        sr.sprite = c_class.sprite;
        anim.runtimeAnimatorController = c_class.ac;
        PlayerManager.Instance.profile.SetPlayerProfileIcon();
    }


    private void SetupWeapon()
    {
        w_sr.sprite = weapon.sprite;

        attackRatePerSecond.AddModifier(weapon.attackRatePerSecond);
        projectileSpeed.AddModifier(weapon.projectileSpeed);
        projectileAmount.AddModifier(weapon.projectilAmountPerShooting);
        cooldownToNextAttack.AddModifier(weapon.cooldownToNextAttack);
    }

    protected override void FixedUpdate()
    {
        base.Update();
    }
    private void DashInputHandler()
    {
        if (Input.GetKeyDown(dashKey))
        {
            dashDir = Mathf.Abs(Input.GetAxisRaw("Horizontal"));
            stateMachine.ChangeState(dashState);
        }
    }
    
    private void FacingDirection()
    {
        if (sr == null) return;

        // player face to left
        if (sr.flipX == true)
        {
            facingDir = -1;
        }
        // player face to right
        else if (sr.flipX == false)
        {
            facingDir = 1;
        }

        facingRight = facingDir == 1 ? true : false;
    }
}
