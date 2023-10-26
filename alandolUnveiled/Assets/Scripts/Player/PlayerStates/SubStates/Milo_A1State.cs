using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milo_A1State : PlayerAbilityState
{
    public bool CanUse { get; set; }

    private float a1Time;

    public Milo_A1State(MainPlayer player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanUse = true;
        player.PlaceViejon();
        a1Time = player.InputHandler.ability1InputStartTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.appliedA1)
            isDone = true;
    }

    public bool CanUse1()
    {
        //Debug.Log(a1Time);
        //Debug.Log(playerData.viejonTime);
        if (a1Time > Time.time - playerData.viejonTime)
        {
            CanUse = true;
        }
        else
        {
            CanUse = false;
        }

        return CanUse;
        //return CanUse && Time.time >= a1Time + playerData.viejonTime;
    }

    public void ResetA1() => CanUse = true;
}
