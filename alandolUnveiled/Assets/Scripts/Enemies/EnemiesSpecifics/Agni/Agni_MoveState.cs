using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agni_MoveState : MoveState
{

    private Agni enemy;
    
    public Agni_MoveState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_Move stateData, Agni enemy) : base(etity, stateMachine, animBoolName, stateData)
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
        if (isDetectingWall || !isDetectingLedge)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
}
