using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiloElViejonState : PlayerAbilityState
{
    public bool CanUse { get; private set; }

    private float a1Time;

    public MiloElViejonState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanUse = false;
        player.InputHandler.UseA1Input();
        player.PlaceViejon();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(player.placed)
            isDone = true;
    }

    public bool CanUse1()
    {
        return CanUse && Time.time >= a1Time + playerData.viejonTime;
    }

    public void ResetA1() => CanUse = true;
}
