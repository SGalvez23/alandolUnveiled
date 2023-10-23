using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnoraAbilityState : AnnoraState
{
    protected bool isDone;
    protected bool isGrounded;
    protected int xInput;
    public AnnoraAbilityState(Annora annora, AnnoraStateMachine stateMachine, AnnoraData annoraData, string animBoolName) : base(annora, stateMachine, annoraData, animBoolName)
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

        isDone = false;
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

        if(isDone)
        {
            if (isGrounded && annora.CurrentVelocity.y > 0.01f)
            {
                stateMachine.ChangeState(annora.IdleState);
            }
            else
            {
                stateMachine.ChangeState(annora.InAirState);
            }
        }
    }
}
