using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    

    protected EnemyStateMachine stateMachine;
    protected Enemy enemy;

    protected Rigidbody2D rb;

    protected float stateTimer;
    protected Vector2 moveDir;
    public bool isAnimationFinishTrigger;
    private string animBoolName;

    protected Transform target;

    public EnemyState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        this.enemy = _enemy;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        enemy.anim.SetBool(animBoolName, true);
        rb = enemy.rb;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }
    public virtual void Exit()
    {
        enemy.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        isAnimationFinishTrigger = true;
    }

    public bool TargetPositionIsInRange(float _range)
    {
        return Vector2.Distance(enemy.transform.position, enemy.target.position) <= _range;
    }
}
