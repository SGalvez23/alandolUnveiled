using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded;
    private int xInput;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool coyoteTime;
    private bool isJumping;
    private bool ability2Input;
    private bool ability3Input;
    private bool ability4Input;

    public PlayerInAirState(MainPlayer player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
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

        CheckCoyoteTime();

        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        jumpInputStop = player.InputHandler.JumpInputStop;
        ability2Input = player.InputHandler.Ability2Input;
        ability3Input = player.InputHandler.Ability3Input;
        ability4Input = player.InputHandler.Ability4Input;

        CheckJumpMultiplier();

        if (isGrounded && player.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.LandedState);
        }
        else if (jumpInput && player.JumpState.CanJump())
        {
            player.InputHandler.HasJumped();
            stateMachine.ChangeState(player.JumpState);
        }
        else if (ability2Input) // && player.RojoVivoState.CanUse2() -- ajustar CanUse2, no funciona
        {
            player.InputHandler.UseA2Input();
            stateMachine.ChangeState(player.RojoVivoState);
        }
        else if (ability3Input)
        {
            player.InputHandler.UseA3Input();
            stateMachine.ChangeState(player.CheveState);
        }
        else if (ability4Input)
        {
            player.InputHandler.UseA4Input();
            stateMachine.ChangeState(player.CarnitaAsadaState);
        }
        else
        {
            player.CheckFlip(xInput);
            player.SetXVelocity(playerData.movementVel * xInput);

            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.CurrentVelocity.x));
            player.Anim.SetFloat("yVelocity", player.CurrentVelocity.y);
        }
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetYVelocity(player.CurrentVelocity.y * playerData.jumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.JumpState.DecreaseJumps();
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;

    public void SetIsJumping() => isJumping = true;
}