using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MiloAimState : PlayerAbilityState
{
    public bool CanUse { get; private set; }
    public Vector3 mouseOnScreen;

    public MiloAimState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.Crosshair();
    }

    public override void Exit()
    {
        base.Exit();

        player.DeleteCrosshair();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Update()
    {
        base.Update();

        if (!player.InputHandler.IsAiming)
        {
            isDone = true;
        }

        if (player.InputHandler.BasicAtkInput)
        {
            player.MiloBasicAtk();
            player.InputHandler.BasicAtkInput = false;
        }

        Vector3 difference = new Vector3(player.InputHandler.MouseInput.x, player.InputHandler.MouseInput.y) - player.transform.position;
        Debug.DrawRay(player.transform.position, difference, Color.red);
    }
}
