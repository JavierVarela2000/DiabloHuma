using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected float xInput;
    protected float yInput;
    protected Rigidbody2D rb;
    protected float stateTimer;

    private string animBoolName;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        player = _player;
        stateMachine = _stateMachine;
        animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        //Debug.Log("Enter on:" + animBoolName);
        player.animator.SetBool(animBoolName, true);
        rb = player.rb;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Z) && player.canCharge && !player.dashCharge)
        {
            rb.AddForce(new(200, 350), ForceMode2D.Impulse);
            player.ChargeDash();
        }

        if (Input.GetKeyDown(KeyCode.X) && player.dashCharge)
        {
            stateMachine.ChangeState(player.dashState);
        }

        if (Input.GetKeyDown(KeyCode.C) && player.canHang && !player.isHanging)
        {
            stateMachine.ChangeState(player.hangState);
        }

        
    }
    public virtual void Exit()
    {
        player.animator.SetBool(animBoolName, false);
    }
}
