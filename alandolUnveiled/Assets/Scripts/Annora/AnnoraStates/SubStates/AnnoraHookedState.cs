using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnoraHookedState : AnnoraAbilityState
{
    public bool CanUse { get; private set; }
    public AnnoraHookedState(Annora annora, AnnoraStateMachine stateMachine, AnnoraData annoraData, string animBoolName) : base(annora, stateMachine, annoraData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();
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

        shot = annora.InputHandler.HookShot;

        if (!shot)
        {
            annora.StopGrapple();
            isDone = true;
        }
        else
        {
            annora.CheckFlip(xInput);
            annora.SetXVelocity(annoraData.movementVel * xInput);

            annora.Anim.SetFloat("xVelocity", Mathf.Abs(annora.CurrentVelocity.x));
            annora.Anim.SetFloat("yVelocity", annora.CurrentVelocity.y);
        }
    }
}
