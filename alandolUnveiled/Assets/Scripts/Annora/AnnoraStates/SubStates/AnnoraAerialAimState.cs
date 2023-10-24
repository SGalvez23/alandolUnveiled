using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        JumpInput = annora.InputHandler.JumpInput;

        if(annora.CurrentVelocity.y > 0.1f)
        {
            stateMachine.ChangeState(annora.LandedState);
        }
        else if(!aiming || xInput == 0)
        {
            stateMachine.ChangeState(annora.InAirState);
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
