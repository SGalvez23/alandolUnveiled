using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected MainPlayer player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected bool isAnimationFinished;

    protected float startTime;

    private string animBoolName;

    PhotonView view;

    public PlayerState(MainPlayer player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;

        view = player.photonView;
    }

    public virtual void Enter()
    {
        DoCheck();
        player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        Debug.Log(animBoolName);
        isAnimationFinished = false;
    }

    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);
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
