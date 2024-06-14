using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpState : PlayerState
{
    public WallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = .5f;
        player.SetVelocity(5 * -player.faceDirection, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(rb.velocity.x / 3, rb.velocity.y / 2);
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer<0 || rb.velocity.y<0)
        {
            stateMachine.ChangeState(player.fallState);
        }
        
    }
}
