using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        
        if (!player.IsWallDetected())
            stateMachine.ChangeState(player.airState);

        //wallslide过程中按空格进入walljump状态
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }
        //反向移动则跳下进入idle状态
        if (xInput != 0 && player.facingDir != xInput)
            stateMachine.ChangeState(player.idleState);
        //按向下键则加速下滑，否则则缓慢下滑
        if (yInput < 0)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y * .7f);


        //落地退出
        if(player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);

    }



}
