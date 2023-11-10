using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    protected D_Idle stateData;

    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;

    protected float idleTime;

    public IdleState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, D_Idle stateData) : base(etity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    
    }  


     public override void EnterState()
    {
        base.EnterState();
        entity.SetVelocity(0f);
        isIdleTimeOver = false;
        SetRandomIdleTime();
    
    }

    public override void ExitState()
    {
        base.ExitState();

        if (flipAfterIdle)
        {
            entity.Flip();
        }
    }


    public override void UpdateState()
    {
        base.UpdateState();
        if(Time.time >= startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }


    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }

    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
        Debug.Log("Llego aqui");
    }

    private void SetRandomIdleTime()
    {
        idleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);
    }




}
