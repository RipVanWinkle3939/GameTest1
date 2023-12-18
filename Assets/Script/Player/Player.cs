using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    
    #region 变量
    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = .2f;
    


    public bool isAttacking { get; set; }
    public bool isBusy { get; private set; }
    [Header("Move info")]
    public float MoveSpeed = 10f;
    public float JumpForce = 10f;

    [Header("Dash info")]
    [SerializeField] private float DashCooldown = 1f;
    private float DashUsageTimer;
    public float DashSpeed = 25f;
    public float DashDuration = 0.2f;
    public float DashDir { get; private set;}



    #endregion
    
    #region 状态
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerCounterAttackState counterAttack { get; private set; }

    #endregion


    protected override void Awake()
    {
        base.Awake();
        //状态机设置
        #region 状态机
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");

        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        #endregion

    }

    protected override void Start()
    {
        base.Start();
        //状态机重置
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();//启动现有状态

        CheckForDashInput();
    }

    public IEnumerator BusyFor(float _seconds)//攻击中不进行其他动作
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);//等待_seconds秒数
        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();//获取动画机中动画结束事件



    public void CheckForDashInput()//冲刺输入检测
    {
        //如果在墙上，不冲刺
        if (IsWallDetected())
            return;
        
        //冲刺时间计时
        DashUsageTimer -= Time.deltaTime;
        //若按键且冲刺CD结束，冲刺
        if (Input.GetKeyDown(KeyCode.LeftShift) && DashUsageTimer < 0)
        {
            DashUsageTimer = DashCooldown;//重置CD
            //冲刺方向
            DashDir = Input.GetAxisRaw("Horizontal");
            if (DashDir == 0)
                DashDir = facingDir;
            stateMachine.ChangeState(dashState);
        }
    }







}
