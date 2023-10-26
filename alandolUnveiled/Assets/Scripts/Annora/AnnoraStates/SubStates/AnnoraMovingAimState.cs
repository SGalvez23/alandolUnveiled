using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnoraMovingAimState : AnnoraAbilityState
{
    public AnnoraMovingAimState(Annora annora, AnnoraStateMachine stateMachine, AnnoraData annoraData, string animBoolName) : base(annora, stateMachine, annoraData, animBoolName)
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

        annora.Crosshair();
    }

    public override void Exit()
    {
        base.Exit();

        annora.DeleteCrosshair();
    }

    public override void Update()
    {
        base.Update();

        JumpInput = annora.InputHandler.JumpInput;

        if (xInput == 0 )
        {
            stateMachine.ChangeState(annora.IdleState);
        }
        else if(!isGrounded)
        {
            stateMachine.ChangeState(annora.AerialAimState);
        }
        else if (aiming && JumpInput && annora.JumpState.CanJump())
        {
            annora.InputHandler.HasJumped();
            stateMachine.ChangeState(annora.JumpState);
        }
        else if (!aiming && xInput != 0)
        {
            stateMachine.ChangeState(annora.MoveState);
        }
        else
        {
            annora.CheckFlip(xInput);
            annora.SetXVelocity(annoraData.movementVel * xInput);

            annora.Anim.SetFloat("xVelocity", Mathf.Abs(annora.CurrentVelocity.x));
            annora.Anim.SetFloat("yVelocity", annora.CurrentVelocity.y);
        }

        Vector3 difference = new Vector3(annora.InputHandler.MousePos.x, annora.InputHandler.MousePos.y) - annora.transform.position;
        Debug.DrawRay(annora.transform.position, difference, Color.red);
    }
}
