using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //起跳持续时间
        stateTimer = .4f;
        //起跳
        player.SetVelocity(5 * -player.facingDir, player.JumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        //起跳后进入air状态
        if (stateTimer < 0)
            stateMachine.ChangeState(player.airState);
        //过程中触地则进入idle状态
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
        
    }
}
