using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    #region Movement
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    #endregion

    #region Jump
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    [SerializeField]
    private float inputBuffer = 0.2f;
    private float jumpInputStartTime;
    #endregion

    #region Abilities
    public bool Ability1Input { get; private set; }
    public bool Ability1InputStop { get; private set; }

    public float ability1InputStartTime;
    #endregion

    private void Update()
    {
        CheckJumpInputHoldTime();
        //CheckAbility1HoldTime();
    }

    #region Inputs
    public void OnMove(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
    }

    public void OnJump(InputAction.CallbackContext context) 
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void OnAbility1(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            Ability1Input = true;
            //Ability1InputStop = false;
            ability1InputStartTime = Time.time;
            //Debug.Log(ability1InputStartTime);
        }
        else if(context.canceled)
        {
            //Ability1InputStop = true;
        }
    }
    #endregion

    #region Checks
    public void HasJumped() => JumpInput = false;

    private void CheckJumpInputHoldTime()
    {
        if(Time.time >= jumpInputStartTime + inputBuffer)
        {
            JumpInput = false;
        }
    }

    public void UseA1Input() => Ability1Input = false;

    private void CheckAbility1HoldTime()
    {
        if(Time.time >= ability1InputStartTime + inputBuffer)
        {
            Ability1Input = false;
        }
    }
    #endregion
}
