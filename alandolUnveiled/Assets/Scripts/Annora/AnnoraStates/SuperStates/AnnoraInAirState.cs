using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnoraInAirState : AnnoraState
{
    private bool isGrounded;
    private int xInput;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool coyoteTime;
    private bool isJumping;

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
        jumpInput = annora.InputHandler.JumpInput;
        jumpInputStop = annora.InputHandler.JumpInputStop;

        CheckJumpMultiplier();

        if(isGrounded && annora.CurrentVelocity.y > 0)
        {
            stateMachine.ChangeState(annora.LandedState);
        }
        else if(jumpInput && annora.JumpState.CanJump())
        {
            stateMachine.ChangeState(annora.JumpState);
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
