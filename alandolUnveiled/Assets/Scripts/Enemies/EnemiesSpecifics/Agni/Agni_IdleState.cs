using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agni_IdleState : IdleState
{
    private Agni enemy;
    public Agni_IdleState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_Idle stateData, Agni enemy) : base(etity, stateMachine, animBoolName, stateData)
    {
        this.stateData = stateData;
    
    }  
    public override void EnterState()
    {
        base.EnterState();

    
    }

    public override void ExitState()
    {
        base.ExitState();

       
    }


    public override void UpdateState()
    {
        base.UpdateState();
        if(isIdleTimeOver)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
}
