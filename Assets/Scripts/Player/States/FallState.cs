using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallState : AirState
{
    public FallState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
        else if (player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);
    }
}
