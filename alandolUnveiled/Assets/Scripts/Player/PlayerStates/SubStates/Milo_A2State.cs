using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milo_A2State : PlayerAbilityState
{
    public bool CanUse { get; private set; }
    private float a2Time;

    public Milo_A2State(MainPlayer player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanUse = false;
        player.InputHandler.UseA2Input();
        player.ApplyA2();
        //player.Throw(player.BasicAtkState.Velocity, player.projectileIndex);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.appliedA2)
        {
            isDone = true;
        }
    }

    public bool CanUse2()
    {
        return CanUse && Time.time >= a2Time + playerData.rojoVivoTime;
    }

    public void ResetA2() => CanUse = true;
}
