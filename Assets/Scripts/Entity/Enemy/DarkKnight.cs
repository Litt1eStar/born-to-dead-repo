using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkKnight : Enemy
{
    public float attackCooldown;
    protected override void Update()
    {
        base.Update();

        if (stateMachine.currentState.TargetPositionIsInRange(enemyData.attackRange))
        {
            if (stateMachine.currentState.isAnimationFinishTrigger)
                return;
            else
                stateMachine.ChangeState(attackState);
        }
    }

    protected override void Die()
    {
        base.Die();
        Destroy(gameObject);
    }
}
