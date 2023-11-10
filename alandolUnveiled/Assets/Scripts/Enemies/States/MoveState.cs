using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_Move stateData;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;

    //Contructor
    public MoveState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_Move stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }  

    public override void EnterState()
    {
        base.EnterState();
        entity.SetVelocity(stateData.movementSpeed);

        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();





    }
    public override void ExitState()
    {
        base.ExitState();
    }
    public override void UpdateState()
    {
        base.UpdateState();
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();

    }
}
