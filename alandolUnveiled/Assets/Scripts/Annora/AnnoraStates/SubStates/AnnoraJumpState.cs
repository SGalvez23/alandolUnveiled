using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnoraJumpState : AnnoraAbilityState
{
    private int amountOfJumpsLeft;

    public AnnoraJumpState(Annora annora, AnnoraStateMachine stateMachine, AnnoraData annoraData, string animBoolName) : base(annora, stateMachine, annoraData, animBoolName)
    {
        amountOfJumpsLeft = annoraData.amountJumps;
    }

    public override void Enter()
    {
        base.Enter();

        annora.SetYVelocity(annoraData.jumpVel);
        isDone = true;
        amountOfJumpsLeft--;
        annora.InAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        if (amountOfJumpsLeft > 0)
        {
            return true;
        }
        else { return false; }
    }

    public void ResetJumps() => amountOfJumpsLeft = annoraData.amountJumps;

    public void DecreaseJumps() => amountOfJumpsLeft--;
}
