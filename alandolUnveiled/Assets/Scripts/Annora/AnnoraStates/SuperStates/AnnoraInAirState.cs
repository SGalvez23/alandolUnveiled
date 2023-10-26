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

        CheckJumpMultiplier();

        
        if(isGrounded && annora.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(annora.LandedState);
        }
        else if(JumpInput && annora.JumpState.CanJump())
        {
            stateMachine.ChangeState(annora.JumpState);
        }
        else if (aiming)
        {
            stateMachine.ChangeState(annora.AerialAimState);
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
            annora.JumpState.DecreaseJumps();
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;

    public void SetIsJumping() => isJumping = true;
}
