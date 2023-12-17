using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyFollowTargetState : EnemyState
{
    public EnemyFollowTargetState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy.target == null) return;

        if (enemy.isDead)
            stateMachine.ChangeState(enemy.deadState);

        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, enemy.target.position, enemy.speed.GetValue() * Time.deltaTime);
    }
}
