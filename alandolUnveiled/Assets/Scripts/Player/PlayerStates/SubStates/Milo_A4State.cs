using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milo_A4State : PlayerAbilityState
{
    public bool CanUse { get; private set; }
    public int carnitaLeft;

    public Milo_A4State(MainPlayer player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        carnitaLeft = playerData.carnitaCant;
    }

    public override void Enter()
    {
        base.Enter();

        player.AudioClips.PlayCarnitaSound();

        CanUse = false;
        player.CookCarnita();
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

    public void ResetA4() => CanUse = true;

    public void DecreaseCarnita() => carnitaLeft--;
}
