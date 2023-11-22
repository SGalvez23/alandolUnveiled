using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnoraAimState : AnnoraAbilityState
{
    public Vector3 mouseOnScreen;
    public AnnoraAimState(Annora annora, AnnoraStateMachine stateMachine, AnnoraData annoraData, string animBoolName) : base(annora, stateMachine, annoraData, animBoolName)
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

        annora.DeleteCrosshair();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Update()
    {
        base.Update();

        aiming = annora.InputHandler.IsAiming;
        JumpInput = annora.InputHandler.JumpInput;
        shot = annora.InputHandler.HookShot;
        isGrappling = annora.IsGrappling;

        if (xInput != 0)
        {
            stateMachine.ChangeState(annora.MovingAimState);
        }
        else if(JumpInput && annora.JumpState.CanJump())
        {
            annora.InputHandler.HasJumped();
            stateMachine.ChangeState(annora.JumpState);
        }
        else if (shot && !isGrappling)
        {
            annora.SetGrapplePoint();
            stateMachine.ChangeState(annora.HookedState);
        }
        else if (!aiming)
        {
            isDone = true;
        }

        Vector3 difference = new Vector3(annora.InputHandler.MousePos.x, annora.InputHandler.MousePos.y) - annora.transform.position;
        Debug.DrawRay(annora.transform.position, difference, Color.red);
    }
}
