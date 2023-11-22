using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Windows;

public class AnnoraAerialAimState : AnnoraAbilityState
{
    public AnnoraAerialAimState(Annora annora, AnnoraStateMachine stateMachine, AnnoraData annoraData, string animBoolName) : base(annora, stateMachine, annoraData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        annora.Crosshair();
    }

    public override void Exit()
    {
        base.Exit();

        annora.Crosshair();
    }

    public override void Update()
    {
        base.Update();

        aiming = annora.InputHandler.IsAiming;
        JumpInput = annora.InputHandler.JumpInput;
        shot = annora.InputHandler.HookShot;
        isGrappling = annora.IsGrappling;

        if (!aiming)
        {
            isDone = true;
        }
        else if (isGrounded && annora.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(annora.LandedState);
        }
        else if (shot && !isGrappling)
        {
            annora.SetGrapplePoint();
            stateMachine.ChangeState(annora.HookedState);
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
