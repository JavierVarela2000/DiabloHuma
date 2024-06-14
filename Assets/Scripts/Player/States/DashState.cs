using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState
{
    public DashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
        player.fireAnimator.SetBool("idle", true);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(rb.velocity.x/3,rb.velocity.y/2);
        player.fireAnimator.SetBool("idle", false);
        player.ChargeDash(false);
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(xInput * player.dashSpeed, player.dashSpeed * yInput);

        if(xInput == 0 && yInput == 0)
        {
            player.SetVelocity(player.faceDirection * player.dashSpeed, rb.velocity.y);
        }

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
