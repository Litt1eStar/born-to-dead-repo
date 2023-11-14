using System.Collections;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashCooldown;
    }

    public override void Update()
    {
        base.Update();

        Vector2 direction = new Vector2(moveDir.x * player.dashForce, moveDir.y * player.dashForce);
        rb.velocity = new Vector2(direction.x * player.dashDir, direction.y);

        if (stateTimer <= 0)
            stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
