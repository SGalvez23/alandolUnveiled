using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milo_A3State : PlayerAbilityState
{
    public bool CanUse { get; private set; }
    private float a3Time;
    private int cheveLeft;

    public Milo_A3State(MainPlayer player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        cheveLeft = playerData.cheveCant;
    }

    public override void Enter()
    {
        base.Enter();
        
        CanUse = false;
        player.InputHandler.UseA3Input();
        player.CookCheve();
        //player.Throw(player.BasicAtkState.Velocity, player.projectileIndex);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(player.appliedA3)
            isDone = true;
    }

    public bool CanUse3()
    {
        //
        return CanUse && Time.time >= a3Time + playerData.cheveTime;
    }

    public bool CanDrink()
    {
        return cheveLeft > 0;
    }

    public void ResetA3() => CanUse = true;

    public void DecreaseCheve() => cheveLeft--;
}
