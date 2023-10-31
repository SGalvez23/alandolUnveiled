using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    private bool JumpInput;
    private bool isGrounded;
    private bool ability1Input;
    private bool ability2Input;
    private bool ability3Input;
    private bool ability4Input;
    private bool canPlaceViejon;
    private bool aiming;

    public PlayerGroundedState(MainPlayer player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
//quitar
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

        if (player.photonView.IsMine)
        {
            xInput = player.InputHandler.NormInputX;
            JumpInput = player.InputHandler.JumpInput;
            ability1Input = player.InputHandler.Ability1Input;
            ability2Input = player.InputHandler.Ability2Input;
            ability3Input = player.InputHandler.Ability3Input;
            ability4Input = player.InputHandler.Ability4Input;

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
            else if (aiming)
            {
                stateMachine.ChangeState(player.BasicAtkState);
            }
            else if (aiming && !isGrounded)
            {
                //falta agregar MiloAerialAimState
                stateMachine.ChangeState(player.InAirState);
            }

            if (ability1Input && canPlaceViejon && player.ViejonState.CanUse)
            {
                player.InputHandler.UseA1Input();
                stateMachine.ChangeState(player.ViejonState);
            }

            if (ability2Input && player.RojoVivoState.CanUse)
            {
                player.InputHandler.UseA2Input();
                stateMachine.ChangeState(player.RojoVivoState);
            }

            if (ability3Input && player.CheveState.CanUse)
            {
                player.InputHandler.UseA3Input();
                stateMachine.ChangeState(player.CheveState);
            }

            if (ability4Input && player.CarnitaAsadaState.CanUse)
            {
                player.InputHandler.UseA4Input();
                stateMachine.ChangeState(player.CarnitaAsadaState);
            }
        }

        
    }
}
