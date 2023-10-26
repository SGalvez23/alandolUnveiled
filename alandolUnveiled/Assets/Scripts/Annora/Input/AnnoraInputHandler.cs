using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnnoraInputHandler : MonoBehaviour
{
    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

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
    public bool IsAiming { get; private set; }
    public Vector2 MousePos { get; private set; }
    public bool HookShot { get; private set; }

    public bool Ability1Input { get; private set; }
    public bool Ability1InputStop { get; private set; }
    public float ability1InputStartTime;

    public bool Ability2Input { get; private set; }
    public bool Ability2InputStop { get; private set; }
    public float ability2InputStartTime;
    #endregion

    private void Update()
    {
        if (view.IsMine)
        {
            CheckJumpInputHoldTime();
        }
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

    public void OnEnterAim(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsAiming = true;
        }
        else if (context.canceled)
        {
            IsAiming = false;
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        MousePos = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            HookShot = true;
        }
        else if (context.canceled)
        {
            HookShot = false;
        }
    }

    public void OnAbility1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Ability1Input = true;
            Ability1InputStop = false;
            ability1InputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            Ability1InputStop = true;
        }
    }

    public void OnAbility2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Ability2Input = true;
            Ability2InputStop = false;
            ability2InputStartTime = Time.time;
            //Debug.Log(Ability2Input);
        }
        if (context.canceled)
        {
            Ability2InputStop = true;
            //Debug.Log(Ability2Input);
        }
    }

    #endregion

    #region Checks
    public void HasJumped() => JumpInput = false;

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputBuffer)
        {
            JumpInput = false;
        }
    }

    #endregion
}
