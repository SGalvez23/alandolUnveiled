using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solstice_IdleState : IdleState
{
    Solstice solstice;
    public Solstice_IdleState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_IdleState stateData, Solstice solstice) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.solstice = solstice;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isPlayerInMinAggroRange)
        {
            stateMachine.ChangeState(solstice.PlayerDetectedState);
        }
        else if(isIdleTimeOver)
        {
            stateMachine.ChangeState(solstice.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
