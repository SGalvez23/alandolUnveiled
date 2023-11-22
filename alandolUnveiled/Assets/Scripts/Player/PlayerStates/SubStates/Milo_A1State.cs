using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milo_A1State : PlayerAbilityState
{
    public bool CanUse { get; private set; }

    private float a1Time;

    public Milo_A1State(MainPlayer player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanUse = false;
        player.PlaceViejon();
        isDone = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }

    public void ResetA1() => CanUse = true;
}
