using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandedState : PlayerGroundedState
{
    public PlayerLandedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if(xInput != 0)
        {
            stateMachine.ChangeState(player.MoveState);
        }
        else if(isAnimationFinished)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }
}
