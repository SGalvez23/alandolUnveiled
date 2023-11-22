using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnoraMoveState : AnnoraGroundedState
{
    public AnnoraMoveState(Annora annora, AnnoraStateMachine stateMachine, AnnoraData annoraData, string animBoolName) : base(annora, stateMachine, annoraData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Update()
    {
        base.Update();

        annora.CheckFlip(xInput);
        annora.SetXVelocity(annoraData.movementVel * xInput);

        if (xInput == 0)
        {
            stateMachine.ChangeState(annora.IdleState);
        }
        else if(aiming) 
        {
            stateMachine.ChangeState(annora.MovingAimState);
        }
        else if(JumpInput)
        {
            stateMachine.ChangeState(annora.JumpState);
        }
    }
}
