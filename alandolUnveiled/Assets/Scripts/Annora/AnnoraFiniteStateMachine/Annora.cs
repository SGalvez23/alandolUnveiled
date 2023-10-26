using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
<<<<<<< HEAD
=======
using Unity.VisualScripting.Dependencies.Sqlite;
>>>>>>> 2a05a6d3076da89c4af55eb72260ac8c6b34a8f9

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
    public AnnoraHookedState HookedState { get; private set; }
    public Annora_A1State ViejonState { get; private set; }
    public Annora_A2State RojoVivoState { get; private set; }
    //public Annora_A3State CheveState { get; private set; }
    //public Annora_A4State CarnitaAsadaState { get; private set; }
    #endregion

    #region Componentes
    public Animator Anim { get; private set; }
    public AnnoraInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rb2D { get; private set; }
    public HookRope HookRope { get; private set; }
    public SpringJoint2D Sj2D { get; private set; }
<<<<<<< HEAD
    public PhotonView view;
=======
>>>>>>> 2a05a6d3076da89c4af55eb72260ac8c6b34a8f9
    //public AnnoraAnimStrings AnimStrings { get; private set; }
    #endregion

    #region Check Transforms
    [SerializeField] 
    private Transform groundCheck;
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
    public Transform hookGunHolder;
    public Transform hookFirePoint;
    public Vector2 grapplePoint;
    public Vector2 grappleDistanceVector;
    bool launchToPoint = true;
    [SerializeField] float launchSpeed = 0.3f;
    public bool infiniteRange = false;
    private float targetDistance = 3;
    private float targetFrequncy = 1;
    protected int maxRange = 250;
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
        HookedState = new AnnoraHookedState(this, StateMachine, annoraData, "hooked");

        FacingDir = 1;
    }

    private void Start()
    {
<<<<<<< HEAD
        view = GetComponent<PhotonView>();

=======
>>>>>>> 2a05a6d3076da89c4af55eb72260ac8c6b34a8f9
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<AnnoraInputHandler>();
        Rb2D = GetComponent<Rigidbody2D>();
        HookRope = GetComponent<HookRope>();
        Sj2D = GetComponent<SpringJoint2D>();
        HookRope.enabled = false;
        Sj2D.enabled = false;

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
<<<<<<< HEAD

        if (view.IsMine)
        {
            CurrentVelocity = Rb2D.velocity;
            StateMachine.CurrentState.Update();

            if (InputHandler.IsAiming)
            {
                crosshair.transform.position = InputHandler.MousePos;

                if (InputHandler.HookShot)
                {
                    SetGrapplePoint();
                }
                else if (!InputHandler.HookShot)
                {
                    Rb2D.gravityScale = 1;
                    Sj2D.enabled = false;
                    HookRope.enabled = false;
                }
            }

            Debug.Log(StateMachine.CurrentState);
        }
        
=======
        CurrentVelocity = Rb2D.velocity;
        StateMachine.CurrentState.Update();

        if (InputHandler.IsAiming)
        {
            crosshair.transform.position = InputHandler.MousePos;

            if(InputHandler.HookShot)
            {
                SetGrapplePoint();
            }
            else if(!InputHandler.HookShot)
            {
                Rb2D.gravityScale = 1;
                Sj2D.enabled = false;
                HookRope.enabled = false;
            }
        }

        Debug.Log(StateMachine.CurrentState);
>>>>>>> 2a05a6d3076da89c4af55eb72260ac8c6b34a8f9
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

    void SetGrapplePoint()
    {
        Vector3 difference = new Vector3(InputHandler.MousePos.x, InputHandler.MousePos.y) - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(hookFirePoint.position, difference.normalized, 1 << 11);
        if (hit.collider != null)
        {
            Debug.Log(hit.transform.gameObject.name);
            if (Vector2.Distance(hit.point, hookFirePoint.position) <= maxRange && hit.collider.CompareTag("Anclaje") || infiniteRange)
            {
                grapplePoint = hit.point;
                grappleDistanceVector = grapplePoint - (Vector2)transform.position;
                HookRope.enabled = true;
            }
        }
    }

    //Funcion que modifica el springJoint2D simular el agarre del gancho
    public void Grapple()
    {
        Sj2D.autoConfigureDistance = false;
        if (!launchToPoint && !Sj2D.autoConfigureDistance)
        {
            Sj2D.distance = targetDistance;
            Sj2D.frequency = targetFrequncy;
        }

        if (!launchToPoint)
        {
            if (Sj2D.autoConfigureDistance)
            {
                Sj2D.autoConfigureDistance = true;
                Sj2D.frequency = 0;
            }

            Sj2D.connectedAnchor = grapplePoint;
            Sj2D.enabled = true;
        }
        else
        {
            Sj2D.connectedAnchor = grapplePoint;
            Vector2 distance = hookFirePoint.position - hookGunHolder.position;
            Sj2D.distance = distance.magnitude;
            Sj2D.frequency = launchSpeed;
            Sj2D.enabled = true;
        }
    }
    #endregion
}
