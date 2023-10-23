using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnoraGroundedState : AnnoraState
{
    protected int xInput;
    private bool JumpInput;
    private bool isGrounded;
    private bool ability1Input;
    private bool ability2Input;
    private bool ability3Input;
    private bool ability4Input;
    private bool aiming;

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

        if(JumpInput && annora.JumpState.CanJump())
        {
            annora.InputHandler.HasJumped();
            stateMachine.ChangeState(annora.JumpState);
        }
    }
}
