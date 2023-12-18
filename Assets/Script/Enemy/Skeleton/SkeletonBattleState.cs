using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Transform player;
    private Enemy_Skeleton enemy;
    private int moveDir;



    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = GameObject.Find("Player").transform;
    }

    public override void Update()
    {
        base.Update();


        /*
        if(enemy == null)
        {
            Debug.LogError("One or more required objects are null");
            return;
        }
        */


        if(enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;//战斗计时器

            //玩家在攻击范围内即攻击
            if(enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if(CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
            }
        
        }
        else
        {
            //玩家脱离距离或战斗计时器归零则返回idlestate
            if(stateTimer <= 0 || Vector2.Distance(enemy.transform.position, player.position) > 15)
                stateMachine.ChangeState(enemy.idleState); 
            
        }
        //面向玩家
        if(player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if(player.position.x < enemy.transform.position.x)
            moveDir = -1;
        
        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);//向玩家移动
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAttack()
    {
        return Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown;//攻击冷却
    }

}
