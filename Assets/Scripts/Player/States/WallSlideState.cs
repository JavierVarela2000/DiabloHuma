using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlideState : PlayerState
{
    public WallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }
        if (xInput !=0 && player.faceDirection != xInput )
        {
            player.SetVelocity(rb.velocity.x + (20 * -1 * player.faceDirection), rb.velocity.y +5);
            stateMachine.ChangeState(player.idleState);
        }
        

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);

        player.SetVelocity(0,rb.velocity.y*0.3f);

       

    }
}
