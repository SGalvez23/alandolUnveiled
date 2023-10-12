using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milo_A3State : PlayerAbilityState
{
    public bool CanUse { get; private set; }
    private float a3Time;

    public Milo_A3State(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanUse = false;
        player.InputHandler.UseA3Input();
        player.CookCheve();

    }

    public override void Update()
    {
        base.Update();

        if(player.appliedA3)
            isDone = true;
    }

    public bool CanUse3()
    {
        return CanUse && Time.time >= a3Time + playerData.cheveTime;
    }

    public void ResetA3() => CanUse = true;
}
