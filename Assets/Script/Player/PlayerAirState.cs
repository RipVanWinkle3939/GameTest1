using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        //检测到墙则进入wallSlide状态
        if (player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);
        //检测到地面则进入idle状态
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
        //空中移动
        if (xInput != 0)
            player.SetVelocity(player.MoveSpeed * xInput * .8f , rb.velocity.y);
    }
}
