using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using Unity.VisualScripting.Dependencies.Sqlite;

public class Annora : MonoBehaviourPunCallbacks
{
    #region Variables de Estado
    public AnnoraStateMachine StateMachine { get; private set; }

    public AnnoraIdleState IdleState { get; private set; }
    public AnnoraMoveState MoveState { get; private set; }
    public AnnoraJumpState JumpState { get; private set; }
    public AnnoraInAirState InAirState { get; private set; }
    public AnnoraLandedState LandedState { get; private set; }
    public AnnoraAimState AimState { get; private set; }
    public AnnoraMovingAimState MovingAimState { get; private set; }
    public AnnoraAerialAimState AerialAimState { get; private set; }
    public Annora_A1State ViejonState { get; private set; }
    public Annora_A2State RojoVivoState { get; private set; }
    //public Annora_A3State CheveState { get; private set; }
    //public Annora_A4State CarnitaAsadaState { get; private set; }
    #endregion

    #region Componentes
    public Animator Anim { get; private set; }
    public AnnoraInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rb2D { get; private set; }
    public LineRenderer LineRenderer { get; private set; }
    //public AnnoraAnimStrings AnimStrings { get; private set; }
    #endregion

    #region Check Transforms
    [SerializeField] 
    private Transform groundCheck;
    public Transform hookFirePoint;
    #endregion

    #region Annora Data
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDir { get; private set; }
    [SerializeField]
    private AnnoraData annoraData;
    private Vector2 annoraVel;
    #endregion

    #region Abilities
    protected Vector3 mouseOnScreen;
    public GameObject crosshair;
    #endregion

    #region UI

    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new AnnoraStateMachine();

        IdleState = new AnnoraIdleState(this, StateMachine, annoraData, "isIdle");
        MoveState = new AnnoraMoveState(this, StateMachine, annoraData, "isMoving");
        JumpState = new AnnoraJumpState(this, StateMachine, annoraData, "inAir");
        InAirState = new AnnoraInAirState(this, StateMachine, annoraData, "inAir");
        LandedState = new AnnoraLandedState(this, StateMachine, annoraData, "landed");
        AimState = new AnnoraAimState(this, StateMachine, annoraData, "aiming");
        MovingAimState = new AnnoraMovingAimState(this, StateMachine, annoraData, "isMoving");
        AerialAimState = new AnnoraAerialAimState(this, StateMachine, annoraData, "inAir");

        FacingDir = 1;
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<AnnoraInputHandler>();
        Rb2D = GetComponent<Rigidbody2D>();
        LineRenderer = GetComponent<LineRenderer>();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = Rb2D.velocity;
        StateMachine.CurrentState.Update();

        if (InputHandler.IsAiming)
        {
            crosshair.transform.position = InputHandler.MousePos;
        }

        Debug.Log(StateMachine.CurrentState);
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region SetFunctions
    public void SetXVelocity(float velocity)
    {
        annoraVel.Set(velocity, CurrentVelocity.y);
        Rb2D.velocity = annoraVel;
        CurrentVelocity = annoraVel;
    }

    public void SetYVelocity(float velocity)
    {
        annoraVel.Set(CurrentVelocity.x, velocity);
        Rb2D.velocity = annoraVel;
        CurrentVelocity = annoraVel;
    }
    #endregion

    #region Check Functions
    public bool CheckIfGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, annoraData.groundCheckRadius, annoraData.whatIsGround);
    }

    public void CheckFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDir)
        {
            Flip();
        }
    }
    #endregion

    #region Other
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void Flip()
    {
        FacingDir *= -1;
        transform.Rotate(0, 180, 0);
    }

    public void Crosshair()
    {
        crosshair.SetActive(true);
    }

    public void DeleteCrosshair()
    {
        crosshair.SetActive(false);
    }
    #endregion
}
