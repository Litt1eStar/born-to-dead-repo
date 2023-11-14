using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    [Header("Weapon")]
    public Weapon weapon;    
    #region Movement
    [Header("Movement Settings")]
    public float dashForce;
    public float dashCooldown;
    public int facingDir { get; private set; }
    public bool facingRight { get; private set; }
    public float dashDir { get; private set; }
    #endregion
    #region References
    [Header("References")]
    public PlayerShoot playerShootSystem;
    public Inventory inventory;
    
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
    public override void Awake()
    {
        
        base.Awake();
        
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Moving");
        dashState = new PlayerDashState(this, stateMachine, "Moving");
    }
    public override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }
    public override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();
        
        FacingDirection();
        DashInputHandler();

        if (weapon != null)
            ApplyStatBasedFromWeaponInventory();
    }

    private void ApplyStatBasedFromWeaponInventory()
    {
        attackRatePerSecond.baseValue = weapon.attackRatePerSecond;
        projectileSpeed.baseValue = weapon.projectileSpeed;
        projectileAmount.baseValue = weapon.projectileAmount;
        cooldownToNextAttack.baseValue = weapon.cooldownToNextAttack;
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
