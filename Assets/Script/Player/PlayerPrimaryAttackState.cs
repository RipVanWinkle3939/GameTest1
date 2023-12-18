using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{

    #region 连击数值
    private int comboCounter;
    private float lastTimeAttacked;
    private float comboWindow = 2f;
    #endregion
    

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isAttacking = true;
        xInput = 0;
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)//连击检测
            comboCounter = 0;

        player.anim.SetInteger("ComboCounter", comboCounter);//播放对应连击的动画
        player.anim.speed = 1;

        float attackDir = player.facingDir;
        if (xInput != 0)
            attackDir = xInput;//临时捕捉玩家操作，属于补丁代码


        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);//攻击时的前越

        stateTimer = .1f;
    }

    public override void Exit()
    {
        
        base.Exit();
        player.StartCoroutine("BusyFor", .15f);//攻击时不进行别的操作

        player.anim.speed = 1;

        player.StartCoroutine(SetIsAttackingFalseAfterDelay(1f)); // 启动协程
        comboCounter++;
        lastTimeAttacked = Time.time;//记录最后一次攻击的时间

    }

    private IEnumerator SetIsAttackingFalseAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 等待指定的时间
        player.isAttacking = false; // 将 isAttacking 设置为 false
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer < 0)
            player.ZeroVelocity();
        //动画机结束事件触发时改变为idle状态
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }

        
    }

}
