using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isDone;
    protected bool isGrounded;
    protected int xInput;

    public PlayerAbilityState(MainPlayer player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();

        isDone = false;
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
        xInput = player.InputHandler.NormInputX;

        if (isDone)
        {
            if (isGrounded && player.CurrentVelocity.y > 0.01f)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else if (isGrounded)
            {
                stateMachine.ChangeState(player.IdleState);
            }
            else //if(!isGrounded)
            {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }
}
