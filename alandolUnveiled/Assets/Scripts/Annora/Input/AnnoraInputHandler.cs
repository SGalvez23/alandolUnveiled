using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class AnnoraInputHandler : MonoBehaviourPunCallbacks
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
    public bool BasicAtkInput { get; private set; }
    public bool BasicAtkInputStop { get; private set; }

    public bool IsAiming { get; private set; }
    public Vector2 MousePos { get; private set; }
    public bool HookShot { get; private set; }

    public bool Ability1Input { get; private set; }
    public bool Ability1InputStop { get; private set; }
    public float ability1InputStartTime;

    public bool Ability2Input { get; private set; }
    public bool Ability2InputStop { get; private set; }
    public float ability2InputStartTime;

    public bool Ability3Input { get; private set; }
    public bool Ability3InputStop { get; private set; }
    public float ability3InputStartTime;

    public bool Ability4Input { get; private set; }
    public bool Ability4InputStop { get; private set; }
    public float ability4InputStartTime;
    #endregion

    PhotonView view;
    PlayerInput inputActions;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        inputActions = GetComponentInParent<PlayerInput>();
    }

    private void Update()
    {
        if(view.IsMine)
        {
            CheckJumpInputHoldTime();
            CheckAbility1HoldTime();
            CheckAbility2HoldTime();
            CheckAbility3HoldTime();
            CheckAbility4HoldTime();
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

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            BasicAtkInput = true;
            BasicAtkInputStop = false;
        }
        else if (context.canceled)
        {
            BasicAtkInput = false;
            BasicAtkInputStop = true;
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

    public void OnAbility3(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Ability3Input = true;
            Ability3InputStop = false;
            ability3InputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            Ability3InputStop = true;
        }
    }

    public void OnAbility4(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Ability4Input = true;
            Ability4InputStop = false;
            ability4InputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            Ability4InputStop = true;
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

    public void UseA1Input() => Ability1Input = false;

    private void CheckAbility1HoldTime()
    {
        if (Time.time >= ability1InputStartTime + inputBuffer)
        {
            Ability1Input = false;
        }
    }

    public void UseA2Input() => Ability2Input = false;

    public void CheckAbility2HoldTime()
    {
        if (Time.time >= ability2InputStartTime + inputBuffer)
        {
            Ability2Input = false;
        }
    }

    public void UseA3Input() => Ability3Input = false;

    public void CheckAbility3HoldTime()
    {
        if (Time.time >= ability3InputStartTime + inputBuffer)
        {
            Ability3Input = false;
        }
    }

    public void UseA4Input() => Ability4Input = false;

    public void CheckAbility4HoldTime()
    {
        if (Time.time >= ability4InputStartTime + inputBuffer)
        {
            Ability4Input = false;
        }
    }
    #endregion
}
