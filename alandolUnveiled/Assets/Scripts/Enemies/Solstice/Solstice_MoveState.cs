using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solstice_MoveState : MoveState
{
    private Solstice solstice;
    public Solstice_MoveState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_MoveState stateData, Solstice solstice) : base(enemy, stateMachine, animBoolName, stateData)
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

        if(!isDetectingLedge || isDetectingWall)
        {
            solstice.IdleState.SetFlipIdle(true);
            stateMachine.ChangeState(solstice.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
