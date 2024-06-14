using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class HangState : PlayerState

{

    public float speed = 15;
    private HingeJoint2D hingeJoint;
    public HangState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        var anchor = player.swingPosition - rb.position;

        //Quaternion rotation = new();
        //rotation.eulerAngles = new Vector3(
        //       player.transform.rotation.x,
        //       player.transform.rotation.y,
        //       rotationZ
        //   );

        //Debug.Log("rotation:"+ rotationZ);
        //player.transform.rotation = rotation;

        rb.freezeRotation = false;
        player.hj.anchor = anchor;
        player.hj.enabled = true;
        player.isHanging = true;
    }

    public override void Exit()
    {
        base.Exit();
        player.transform.rotation = Quaternion.identity;
        player.hj.enabled = false;
        rb.freezeRotation = true;
        player.isHanging = false;
    }

    public override void Update()
    {
        base.Update();
        float angularVelocity = 0;
        float currentAngle = player.hj.jointAngle;


       if (xInput != 0)
        {
            angularVelocity = player.moveSpeed * xInput * 110;
        }

        player.SetAngularVelocity(angularVelocity);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetVelocity(rb.velocity.y+5,rb.velocity.y+10);
            stateMachine.ChangeState(player.fallState);
        }

        Debug.Log("Ángulo actual: " + currentAngle);

    }
}
