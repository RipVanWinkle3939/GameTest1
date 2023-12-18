using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //冲刺持续时间重置
        stateTimer = player.DashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        //冲刺结束，速度归零
        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        //冲刺途中遇到墙壁进入wallSlide状态
        if (!player.IsGroundDetected() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlideState);
        //冲刺速度
        player.SetVelocity(player.DashSpeed * player.DashDir, 0);
        if (stateTimer < 0)//冲刺结束
        {
            stateMachine.ChangeState(player.idleState);
        }
    }


}
