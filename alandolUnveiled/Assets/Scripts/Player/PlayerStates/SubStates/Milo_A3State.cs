using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milo_A3State : PlayerAbilityState
{
    public bool CanUse { get; private set; }
    private int cheveLeft;

    public Milo_A3State(MainPlayer player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        cheveLeft = playerData.cheveCant;
    }

    public override void Enter()
    {
        base.Enter();

        player.AudioClips.PlayCheveSound();

        CanUse = false;
        player.CookCheve();
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

    public bool CanDrink()
    {
        return cheveLeft > 0;
    }

    public void ResetA3() => CanUse = true;

    public void DecreaseCheve() => cheveLeft--;
}
