using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
        //起跳时给一个向上的速度
        rb.velocity = new Vector2(rb.velocity.x, player.JumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        //检测到墙则进入wallSlide状态
        if (player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);
        //跳到最高点时转为air状态下落
        if (rb.velocity.y < 0.1f)
            stateMachine.ChangeState(player.airState);
        //空中进行削减过的移动
        if (xInput != 0)
            player.SetVelocity(player.MoveSpeed * xInput * .8f , rb.velocity.y);
    }
}
