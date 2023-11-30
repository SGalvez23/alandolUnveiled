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

        player.AudioClips.PlayrRojoVivoSound();

        CanUse = false;
        player.ApplyA2();
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

    public void ResetA2() => CanUse = true;
}
