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
    protected bool basicAtkInput;
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

        if (annora.photonView.IsMine)
        {
            xInput = annora.InputHandler.NormInputX;
            JumpInput = annora.InputHandler.JumpInput;
            ability1Input = annora.InputHandler.Ability1Input;
            ability2Input = annora.InputHandler.Ability2Input;
            ability3Input = annora.InputHandler.Ability3Input;
            ability4Input = annora.InputHandler.Ability4Input;

            basicAtkInput = annora.InputHandler.BasicAtkInput;
            aiming = annora.InputHandler.IsAiming;

            
            if (basicAtkInput)
            {
                stateMachine.ChangeState(annora.BasickAtkState);
            }
            else if (aiming)
            {
                stateMachine.ChangeState(annora.AimState);
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
            else if (ability1Input && annora.CamoState.CanUse)
            {
                annora.InputHandler.UseA1Input();
                stateMachine.ChangeState(annora.CamoState);
            }
            else if (ability2Input && annora.FrenesiState.CanUse)
            {
                annora.InputHandler.UseA2Input();
                stateMachine.ChangeState(annora.FrenesiState);
            }
            else if (ability3Input && annora.ApretonState.CanUse)
            {
                annora.InputHandler.UseA3Input();
                stateMachine.ChangeState(annora.ApretonState);
            }
            else if (ability4Input && annora.MuerteCerteState.CanUse)
            {
                annora.InputHandler.UseA4Input();
                stateMachine.ChangeState(annora.MuerteCerteState);
            }
        }
    }
}
