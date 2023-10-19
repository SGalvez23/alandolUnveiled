using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milo_A1State : PlayerAbilityState
{
    public bool CanUse { get; private set; }

    private float a1Time;

    public Milo_A1State(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        if (player.appliedA1)
            isDone = true;
    }

    public bool CanUse1()
    {
        return CanUse && Time.time >= a1Time + playerData.viejonTime;
    }

    public void ResetA1() => CanUse = true;
}
