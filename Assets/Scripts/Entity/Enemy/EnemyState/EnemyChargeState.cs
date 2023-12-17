using System.Collections;
using UnityEngine;

public class EnemyChargeState : EnemyState
{
    float blinkCooldown = .5f;
    public EnemyChargeState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = blinkCooldown;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        enemy.sr.color = Color.red;
        yield return new WaitForSeconds(blinkCooldown);
        enemy.sr.color = Color.white;
        yield return new WaitForSeconds(blinkCooldown);
        enemy.sr.color = Color.red;
        yield return new WaitForSeconds(blinkCooldown);
        enemy.sr.color = Color.white;

        yield return null;
    }
}
