using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnoraState
{
    protected Annora annora;
    protected AnnoraStateMachine stateMachine;
    protected AnnoraData annoraData;

    protected bool isAnimationFinished;
    protected float startTime;
    private string animBoolName;

    public AnnoraState(Annora annora, AnnoraStateMachine stateMachine, AnnoraData annoraData, string animBoolName)
    {
        this.annora = annora;
        this.stateMachine = stateMachine;
        this.annoraData = annoraData;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoCheck();
        annora.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        //Debug.Log(stateMachine.CurrentState);
        isAnimationFinished = false;
    }

    public virtual void Exit()
    {
        annora.Anim.SetBool(animBoolName, false);
    }

    public virtual void Update()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoCheck();
    }

    public virtual void DoCheck()
    {

    }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
