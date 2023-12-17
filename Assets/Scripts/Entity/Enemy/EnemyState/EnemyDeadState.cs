using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyState
{
    private float timeToDestroy = 3f;
    public EnemyDeadState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = timeToDestroy;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //enemy.GetComponent<CapsuleCollider2D>().isTrigger = true;
        Destroy(enemy.GetComponent<CapsuleCollider2D>());
        if (stateTimer <= 0)
            Destroy(enemy.gameObject);
    }
}
