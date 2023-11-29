using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solstice_ChargeState : ChargeState
{
    private Solstice solstice;
    public Solstice_ChargeState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_ChargeState stateData, Solstice solstice) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.solstice = solstice;
    }

    public override void DoChecks()
    {
        base.DoChecks();
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

        if(!isDetectingLedge || isDetectingWall)
        {
            stateMachine.ChangeState(solstice.LookForPlayerState);
        }
        else if (isChargeTimeOver)
        {
            //attack

            if(isPlayerInMinAggroRange)
            {
                stateMachine.ChangeState(solstice.PlayerDetectedState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
