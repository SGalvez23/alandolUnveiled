using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnoraJumpState : AnnoraAbilityState
{
    private int totalJumps;
    private int amountOfJumpsLeft;

    public AnnoraJumpState(Annora annora, AnnoraStateMachine stateMachine, AnnoraData annoraData, string animBoolName) : base(annora, stateMachine, annoraData, animBoolName)
    {
        amountOfJumpsLeft = annoraData.totalJumps;
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

    public void ResetJumps() => amountOfJumpsLeft = annoraData.totalJumps;

    public void DecreaseJumps() => amountOfJumpsLeft--;
}
