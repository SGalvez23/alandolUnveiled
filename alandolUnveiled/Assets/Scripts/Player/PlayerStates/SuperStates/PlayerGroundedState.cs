using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    private bool JumpInput;
    private bool isGrounded;
    private bool ability1Input;
    private bool canPlaceViejon;
    private bool basicInput;
    private bool aiming;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoCheck()
    {
        base.DoCheck();

        isGrounded = player.CheckIfGrounded();
        canPlaceViejon = player.CheckAbility1();
    }

    public override void Enter()
    {
        base.Enter();

        player.JumpState.ResetJumps();
        player.ViejonState.ResetA1();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void Update()
    {
        base.Update();

        xInput = player.InputHandler.NormInputX;
        JumpInput = player.InputHandler.JumpInput;
        ability1Input = player.InputHandler.Ability1Input;
        //basicInput = player.InputHandler.BasicAtkInput;
        aiming = player.InputHandler.IsAiming;

        if (JumpInput && player.JumpState.CanJump())
        {
            player.InputHandler.HasJumped();
            stateMachine.ChangeState(player.JumpState);
        }
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.InAirState);
        }

        if (ability1Input && player.ViejonState.CanUse1() && canPlaceViejon)
        {
            player.InputHandler.UseA1Input();
            stateMachine.ChangeState(player.ViejonState);
        }

        if(aiming)
        {
            stateMachine.ChangeState(player.BasicAtkState);
        }
    }
}
