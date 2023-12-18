using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if (Input.GetKeyDown(KeyCode.Q))
            stateMachine.ChangeState(player.counterAttack);
        //按键攻击
        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.primaryAttack);

        //加了个落地后取消滑行的设定，我很讨厌这个手感
        if (player.IsGroundDetected() && !player.isAttacking)
            player.SetVelocity(0 , rb.velocity.y);
        //如果不在地上则进入air状态
        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);
        //在地上时按空格起跳
        if (Input.GetButtonDown("Jump") && player.IsGroundDetected())
            stateMachine.ChangeState(player.jumpState);

    }

}
