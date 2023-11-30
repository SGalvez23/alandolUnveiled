using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solstice_PlayerDetectedState : PlayerDetectedState
{
    private Solstice solstice;
    public Solstice_PlayerDetectedState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_PlayerDetectedState stateData, Solstice solstice) : base(enemy, stateMachine, animBoolName, stateData)
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

        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(solstice.MeleeAttackState);
        }
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(solstice.ChargeState);
        }
        else if(!isPlayerInMaxAggroRange)
        {
            stateMachine.ChangeState(solstice.LookForPlayerState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
