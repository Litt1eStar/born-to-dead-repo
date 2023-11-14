using System.Collections;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter to PlayerMoveState");
    }

    public override void Update()
    {
        base.Update();

        if (moveDir == Vector2.zero) stateMachine.ChangeState(player.idleState);

        if (moveDir.x < 0)
        {            
            player.sr.flipX = true;
        }
        else
        {
            player.sr.flipX = false;
        }

        //Debug.Log(player.speed.GetValue());
        rb.velocity = new Vector2(moveDir.x * player.speed.GetValue(), moveDir.y * player.speed.GetValue());
    }

    public override void Exit()
    {
        base.Exit();
    }
}
