using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    public float lifes = 3;
    [Header("Move Info")]
    public float moveSpeed = 12f;
    public float jumpForce = 10f;
    public float dashSpeed;
    public float dashDuration;
    public Animator fireAnimator;

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask layerGround;


    public int faceDirection { get; private set; } = 1;
    private bool faceRight = true;

    public bool canCharge = false;
    public bool dashCharge { get; private set; } = false;

    public bool canHang = false;
    public bool isHanging = false;

    #region Components
    public PlayerStateMachine stateMachine { get; private set; }
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public HingeJoint2D hj { get; private set; }
    public Vector2 swingPosition { get; private set; }
    #endregion

    #region States
    public IdleState idleState { get; private set; }
    public MoveState moveState { get; private set; }
    public FallState fallState { get; private set; }
    public JumpState jumpState { get; private set; }
    public DashState dashState { get; private set; }
    public HangState hangState { get; private set; }
    public WallSlideState wallSlideState { get; private set; }
    public WallJumpState wallJumpState { get; private set; }
    #endregion
    private void Awake()
    {
        stateMachine = new();
        idleState = new(this, stateMachine, "Idle");
        moveState = new(this, stateMachine, "Move");
        jumpState = new(this, stateMachine, "Jump");
        fallState = new(this, stateMachine, "Fall");
        dashState = new(this, stateMachine, "Dash");
        hangState = new(this, stateMachine, "Hang");
        wallSlideState = new(this, stateMachine, "WallSlide");
        wallJumpState = new(this, stateMachine, "Jump");
    }

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        hj = GetComponent<HingeJoint2D>();
        stateMachine.Initialize(idleState);
    }

    public float timer;
    private void Update()
    {
        stateMachine.currentState.Update();
        timer -= Time.deltaTime;
        //if (timer < 0 && Input.GetKeyDown(KeyCode.R))
        //{
        //    dashCharge = true;
        //}

    }

    public void SetVelocity(float _x, float _y)
    {
        rb.velocity = new Vector2(_x, _y);
        FlipController(_x);
    }
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, layerGround);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * faceDirection, wallCheckDistance, layerGround);
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

    public void Flip()
    {
        faceDirection *= -1;
        faceRight = !faceRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float _x)
    {
        if (_x > 0 && !faceRight) Flip();
        else if (_x < 0 && faceRight) Flip();

    }

    

    public void ChargeDash()
    {
        dashCharge = true;
    }

    public void ChargeDash(bool value)
    {
        dashCharge = value;
    }

    public void SetAngularVelocity(float speed)
    {
        rb.angularVelocity = speed;
    }

    public void changeSwingPosition(Vector2 position)
    {

        swingPosition = position;
    }


    public void restarVida()
    {
        //agregar logico
    }

   


}
