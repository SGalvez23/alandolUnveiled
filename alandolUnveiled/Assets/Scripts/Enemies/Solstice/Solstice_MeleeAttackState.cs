using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solstice_MeleeAttackState : MeleeAttackState
{
    private Solstice solstice;

    public Solstice_MeleeAttackState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, Data_MeleeAttackState stateData, Solstice solstice) : base(enemy, stateMachine, animBoolName, attackPosition, stateData)
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

        solstice.attackDetails.damageAmount = stateData.attackDamage;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isAnimationFinished)
        {
            if (isPlayerInMinAggroRange)
            {
                stateMachine.ChangeState(solstice.PlayerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(solstice.LookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
