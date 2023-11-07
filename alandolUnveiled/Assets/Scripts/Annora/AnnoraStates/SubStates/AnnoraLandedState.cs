using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnoraLandedState : AnnoraGroundedState
{
    public AnnoraLandedState(Annora annora, AnnoraStateMachine stateMachine, AnnoraData annoraData, string animBoolName) : base(annora, stateMachine, annoraData, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if(xInput != 0)
        {
            stateMachine.ChangeState(annora.MoveState);
        }
        else if(isAnimationFinished)
        {
            stateMachine.ChangeState(annora.IdleState);
        }
        else if (aiming)
        {
            stateMachine.ChangeState(annora.AimState);
        }
    }
}
