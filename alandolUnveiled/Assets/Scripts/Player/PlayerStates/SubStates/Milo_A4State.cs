using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Milo_A4State : PlayerAbilityState
{
    public bool CanUse { get; private set; }
    private float a4Time;

    public Milo_A4State(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanUse = false;
        player.InputHandler.UseA4Input();
        player.CookCarnita();
        //player.Throw(player.BasicAtkState.Velocity, player.projectileIndex);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(player.appliedA4)
            isDone = true;
    }

    public bool CanUse4()
    {
        return CanUse && Time.time >= a4Time + playerData.carnitaTime;
    }

    public void ResetA4() => CanUse = true;
}
