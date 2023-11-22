using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnoraInAirState : AnnoraState
{
    protected bool isGrounded;
    protected int xInput;
    protected bool JumpInput;
    protected bool jumpInputStop;
    protected bool coyoteTime;
    protected bool isJumping;
    protected bool aiming;
    protected bool shot;

    protected bool basicAtkInput;
    protected bool ability1Input;
    protected bool ability2Input;
    protected bool ability3Input;
    protected bool ability4Input;

    public AnnoraInAirState(Annora annora, AnnoraStateMachine stateMachine, AnnoraData annoraData, string animBoolName) : base(annora, stateMachine, annoraData, animBoolName)
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

        CheckCoyoteTime();

        xInput = annora.InputHandler.NormInputX;
        JumpInput = annora.InputHandler.JumpInput;
        jumpInputStop = annora.InputHandler.JumpInputStop;
        aiming = annora.InputHandler.IsAiming;
        shot = annora.InputHandler.HookShot;
        basicAtkInput = annora.InputHandler.BasicAtkInput;
        ability1Input = annora.InputHandler.Ability1Input;
        ability2Input = annora.InputHandler.Ability2Input;
        ability3Input = annora.InputHandler.Ability3Input;
        ability4Input = annora.InputHandler.Ability4Input;

        CheckJumpMultiplier();


        if (basicAtkInput)
        {
            stateMachine.ChangeState(annora.BasickAtkState);
        }
        else if (aiming)
        {
            stateMachine.ChangeState(annora.AerialAimState);
        }
        else if (isGrounded && annora.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(annora.LandedState);
        }
        else if(JumpInput && annora.JumpState.CanJump())
        {
            stateMachine.ChangeState(annora.JumpState);
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
        else
        {
            annora.CheckFlip(xInput);
            annora.SetXVelocity(annoraData.movementVel * xInput);

            annora.Anim.SetFloat("xVelocity", Mathf.Abs(annora.CurrentVelocity.x));
            annora.Anim.SetFloat("yVelocity", annora.CurrentVelocity.y);
        }
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                annora.SetYVelocity(annora.CurrentVelocity.y * annoraData.jumpHeightMultiplier);
                isJumping = false;
            }
            else if (annora.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + annoraData.coyoteTime)
        {
            coyoteTime = false;
            //annora.JumpState.DecreaseJumps();
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;

    public void SetIsJumping() => isJumping = true;
}
