using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
//using Unity.VisualScripting.Dependencies.Sqlite;
//using UnityEditor.U2D.Aseprite;
=======
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor.U2D.Aseprite;
>>>>>>> 2a05a6d3076da89c4af55eb72260ac8c6b34a8f9
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MiloAimState : PlayerAbilityState
{
    public bool CanUse { get; private set; }
    public Vector2 Velocity { get; private set; }
    public Vector3 mouseOnScreen;

    public MiloAimState(MainPlayer player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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

        Velocity = (player.InputHandler.StartingMousePos - player.InputHandler.MouseInput) * playerData.launchForce;

        if (player.InputHandler.BasicAtkInput)
        {
            player.Throw(Velocity, player.ProjectileIndex);
            player.InputHandler.BasicAtkInput = false;
        }

        Vector3 difference = new Vector3(player.InputHandler.MouseInput.x, player.InputHandler.MouseInput.y) - player.transform.position;
        Debug.DrawRay(player.transform.position, difference, Color.red);
    }
}
