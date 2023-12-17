using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    private float attackCooldown = 5f;
    public EnemyAttackState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
        _enemy.OnExit += ExitHandler;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = attackCooldown;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (!isAnimationFinishTrigger)
            return;

        if (isAnimationFinishTrigger && !TargetPositionIsInRange(enemy.enemyData.attackRange))
        {
            stateMachine.ChangeState(enemy.followTargetState);
        }
    }

    private void ExitHandler()
    {
        AnimationFinishTrigger();
        Debug.Log("AnimationFinish");
    }
}
