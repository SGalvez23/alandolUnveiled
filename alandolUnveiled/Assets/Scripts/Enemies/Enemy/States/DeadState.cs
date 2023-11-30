using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    protected Data_DeadState stateData;

    public DeadState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_DeadState stateData) : base(enemy, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        //GameObject.Instantiate(stateData.deathEffect, enemy.transform.position, stateData.deathEffect.transform.rotation);
        enemy.Death();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
