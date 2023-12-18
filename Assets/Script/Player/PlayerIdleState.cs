using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.ZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        //防止在墙边向墙移动时反复改变状态
        if (player.IsWallDetected() && player.facingDir == xInput)
            return;

        //进入move状态
        if (xInput != 0 && !player.isBusy)
            stateMachine.ChangeState(player.moveState);



    }

}
