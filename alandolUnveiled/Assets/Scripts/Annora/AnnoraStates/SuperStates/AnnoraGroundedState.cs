using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnoraGroundedState : AnnoraState
{
    protected int xInput;
    protected bool JumpInput;
    protected bool isGrounded;
    protected bool ability1Input;
    protected bool ability2Input;
    protected bool ability3Input;
    protected bool ability4Input;
    protected bool aiming;

    public AnnoraGroundedState(Annora annora, AnnoraStateMachine stateMachine, AnnoraData annoraData, string animBoolName) : base(annora, stateMachine, annoraData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

        isGrounded = annora.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();

        annora.JumpState.ResetJumps();
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

        xInput = annora.InputHandler.NormInputX;
        JumpInput = annora.InputHandler.JumpInput;
        ability1Input = annora.InputHandler.Ability1Input;
        ability2Input = annora.InputHandler.Ability2Input;

        aiming = annora.InputHandler.IsAiming;

        if (xInput == 0)
        {
            stateMachine.ChangeState(annora.IdleState);
        }
        else if(JumpInput && annora.JumpState.CanJump())
        {
            annora.InputHandler.HasJumped();
            stateMachine.ChangeState(annora.JumpState);
        }
        else if(!isGrounded)
        {
            annora.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(annora.InAirState);
        }
    }
}
