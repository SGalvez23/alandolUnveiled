using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft;

    public PlayerJumpState(MainPlayer player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfJumpsLeft = playerData.amountJumps;
    }

    public override void Enter()
    {
        base.Enter();

        player.SetYVelocity(playerData.jumpVel);
        isDone = true;
        amountOfJumpsLeft--;
        player.InAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        if (amountOfJumpsLeft > 0)
        {
            return true;
        }
        else { return false; }
    }

    public void ResetJumps() => amountOfJumpsLeft = playerData.amountJumps;

    public void DecreaseJumps() => amountOfJumpsLeft--;
}