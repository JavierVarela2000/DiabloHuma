using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : PlayerState
{
    public AirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

       

        if (xInput != 0)
            player.SetVelocity(xInput * player.moveSpeed*0.8f, rb.velocity.y);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
