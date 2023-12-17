using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Enemy : Entity
{
    public event Action OnExit;
    private AnimationEventHandler eventHandler;

    public Transform target { get; private set; }
    public EnemySO enemyData;
    [SerializeField] private Transform posToSpawnText;
    [SerializeField] private Transform posToDestroy;
    protected float timer;
    public bool isDead { get; set; }
    public bool canTurnDir { get; set; }
    #region State
    public EnemyStateMachine stateMachine { get; private set; }
    public EnemyIdleState idleState { get; private set; }
    public EnemyFollowTargetState followTargetState { get; private set; }
    public EnemyAttackState attackState { get; private set; }
    public EnemyDeadState deadState { get; private set; }

    public EnemyChargeState chargeState { get; private set; }

    #endregion

    private void OnEnable()
    {
        eventHandler.OnFinish += Exit;
    }

    private void OnDisable()
    {
        eventHandler.OnFinish -= Exit;
    }
    protected override void Awake()
    {
        base.Awake();
        eventHandler = GetComponentInChildren<AnimationEventHandler>();

        stateMachine = new EnemyStateMachine();

        idleState = new EnemyIdleState(this, stateMachine, "Idle");
        followTargetState = new EnemyFollowTargetState(this, stateMachine, "Move");
        attackState = new EnemyAttackState(this, stateMachine, "Attack");
        deadState = new EnemyDeadState(this, stateMachine, "Dead");
        chargeState = new EnemyChargeState(this, stateMachine, "Idle");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(followTargetState);

        target = PlayerManager.Instance.player.GetComponent<Transform>();
        Introduction();
    }
    protected override void FixedUpdate()
    {

        base.FixedUpdate();
    }

    protected override void Update()
    {

        base.Update();

        stateMachine.currentState.Update();

        canTurnDir = isDead == false? true : false;
       
        TurnDirection();
    }

    private void Exit()
    {
        OnExit?.Invoke();
    }

    protected virtual void Introduction()
    {
        Debug.Log($"Hi My Name is {this.gameObject.name} | Hp : {currentHealth}");
    }

    private void TurnDirection()
    {
        if (!canTurnDir) return;

        if (transform.position.x > target.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }

    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.TakeDamage(strength.GetValue());

            Debug.Log("Current Player Hp : " + player.currentHealth + " Enemy Damage : " + strength.GetValue() + " | Enemy Tyep = " + this.gameObject.name);
        }
        else
            Debug.Log("Player not found");
    }

    public override void TakeDamage(float _damage)
    {
        base.TakeDamage(_damage);
        DamagePopup.Create(posToSpawnText.position, _damage, posToDestroy);
    }
    protected override void Die()
    {
        WaveTracker.Instance.IncreseEnemyDeadCount();
        GetComponent<LootBag>().InstantiateExpLootBag();
        isDead = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, enemyData.attackRange);
        Gizmos.color = Color.red;
    }
}
