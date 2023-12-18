using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    #region 定义变量
    protected PlayerStateMachine stateMachine;
    protected Player player;
    
    protected Rigidbody2D rb;
    protected float xInput;
    protected float yInput;
    private string animBoolName;
    protected float stateTimer;
    protected bool triggerCalled;

    #endregion

    
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        //初始化状态
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }
    
    public virtual void Enter()
    {
        //动画机true
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        //动画机事件初始化
        triggerCalled = false;
    }
    
    public virtual void Update()
    {
        //计时器
        stateTimer -= Time.deltaTime;
        //获取xy按键
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        //动画机yVelocity赋值，判断上升与下落
        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void Exit()
    {
        //动画机false
        player.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        //动画机结束触发事件
        triggerCalled = true;
    }

}
