using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Annora : MonoBehaviourPunCallbacks
{
    #region Variables de Estado
    public AnnoraStateMachine StateMachine { get; private set; }

    public AnnoraIdleState IdleState { get; private set; }
    public AnnoraMoveState MoveState { get; private set; }
    public AnnoraJumpState JumpState { get; private set; }
    public AnnoraInAirState InAirState { get; private set; }
    public AnnoraLandedState LandedState { get; private set; }
    public AnnoraBasicAtkState BasickAtkState { get; private set; }
    public AnnoraAimState AimState { get; private set; }
    public AnnoraMovingAimState MovingAimState { get; private set; }
    public AnnoraAerialAimState AerialAimState { get; private set; }
    public AnnoraHookedState HookedState { get; private set; }
    public Annora_A1State CamoState { get; private set; }
    public Annora_A2State FrenesiState { get; private set; }
    public Annora_A3State ApretonState { get; private set; }
    public Annora_A4State MuerteCerteState { get; private set; }
    #endregion

    #region Componentes
    public Animator Anim { get; private set; }
    public AnnoraInputHandler InputHandler { get; private set; }
    public Rigidbody2D Rb2D { get; private set; }
    public HookRope HookRope { get; private set; }
    public SpringJoint2D Sj2D { get; private set; }
    public SpriteRenderer SpriteRend { get; private set; }
    public AnnoraAudioClips AudioClips { get; private set; }
    public PhotonView view;
    public CheckpointManager CheckpointManager { get; private set; }
    private AttackDetails attackDetails;
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
    //regresar a private
    public AnnoraData annoraData;
    private Vector2 annoraVel;

    public float actualHealth;
    public int acutalLives;
    #endregion

    #region Abilities
    [Header("Gancho")]
    protected Vector3 mouseOnScreen;
    public GameObject crosshair;
    public Transform hookGunHolder;
    public Transform hookFirePoint;
    public bool IsGrappling { get; private set; }
    public Vector2 grapplePoint;
    public Vector2 grappleDistanceVector;
    bool launchToPoint = true;
    [SerializeField] float launchSpeed = 0.3f;
    public bool infiniteRange = true;
    private float targetDistance = 3;
    private float targetFrequncy = 1;
    protected int maxRange = 250;

    public LayerMask enemies;
    public bool CanGrabEnemies { get; private set; }

    AbilityHolder abilityHolder;
    
    [Header("Abilities")]
    public GameObject basicHitbox;
    public GameObject A4Hitbox;
    public GameObject A4Effect;

    //[SerializeField]Material DefMat;
    //[SerializeField]Material A1Mat;
    #endregion

    #region UI

    AnnoraHUD annoraHUD;

    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        annoraData.health = 100;
        StateMachine = new AnnoraStateMachine();

        IdleState = new AnnoraIdleState(this, StateMachine, annoraData, "isIdle");
        MoveState = new AnnoraMoveState(this, StateMachine, annoraData, "isMoving");
        JumpState = new AnnoraJumpState(this, StateMachine, annoraData, "inAir");
        BasickAtkState = new AnnoraBasicAtkState(this, StateMachine, annoraData, "basicAtk");
        InAirState = new AnnoraInAirState(this, StateMachine, annoraData, "inAir");
        LandedState = new AnnoraLandedState(this, StateMachine, annoraData, "landed");
        AimState = new AnnoraAimState(this, StateMachine, annoraData, "aiming");
        MovingAimState = new AnnoraMovingAimState(this, StateMachine, annoraData, "isMoving");
        AerialAimState = new AnnoraAerialAimState(this, StateMachine, annoraData, "inAir");
        HookedState = new AnnoraHookedState(this, StateMachine, annoraData, "hooked");
        CamoState = new Annora_A1State(this, StateMachine, annoraData, "camo");
        FrenesiState = new Annora_A2State(this, StateMachine, annoraData, "frenesi");
        ApretonState = new Annora_A3State(this, StateMachine, annoraData, "apreton");
        MuerteCerteState = new Annora_A4State(this, StateMachine, annoraData, "muerteCerte");

        FacingDir = 1;
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<AnnoraInputHandler>();
        Rb2D = GetComponent<Rigidbody2D>();
        HookRope = GetComponent<HookRope>();
        Sj2D = GetComponent<SpringJoint2D>();
        SpriteRend = GetComponent<SpriteRenderer>();
        AudioClips = GetComponentInChildren<AnnoraAudioClips>();
        HookRope.enabled = false;
        Sj2D.enabled = false;
        IsGrappling = false;

        actualHealth = annoraData.health;
        acutalLives = annoraData.vidas;
        CheckpointManager = FindObjectOfType<CheckpointManager>();

        annoraHUD = GetComponent<AnnoraHUD>();
        abilityHolder = GetComponent<AbilityHolder>();
        CamoState.ResetA1();
        FrenesiState.ResetA2();
        ApretonState.ResetA3();
        MuerteCerteState.ResetA4();

        view = GetComponent<PhotonView>();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        if(view.IsMine)
        {
            CurrentVelocity = Rb2D.velocity;
            StateMachine.CurrentState.Update();

            if (InputHandler.IsAiming)
            {
                crosshair.transform.position = InputHandler.MousePos;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                actualHealth -= 20;
            }

            if (actualHealth <= 0)
            {
                Death();
            }
        }
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
    public void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void Flip()
    {
        FacingDir *= -1;
        transform.Rotate(0, 180, 0);
    }

    public void Death()
    {
        acutalLives -= 1;
        gameObject.SetActive(false);
        if (acutalLives >= 0)
        {
            CheckpointManager.LoadCheckpoint();
            actualHealth = 100;
            gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("GameOver");
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjs = Physics2D.OverlapCircleAll(basicHitbox.transform.position, 5f, enemies);

        attackDetails.damageAmount = annoraData.basicAtkDmg;
        attackDetails.position = transform.position;

        foreach(Collider2D collider in detectedObjs)
        {
            collider.transform.SendMessage("Damage", attackDetails);
        }
    }

    private void Damage(AttackDetails attackDetails)
    {
        actualHealth -= attackDetails.damageAmount;

        if (attackDetails.position.x < transform.position.x)
        {
            //knockback
        }
        else
        {
            //knockback
        }
    }
    #endregion


    #region Gancho

    public void Crosshair()
    {
        crosshair.SetActive(true);
    }

    public void DeleteCrosshair()
    {
        crosshair.SetActive(false);
    }

    public void StopGrapple()
    {
        
        Rb2D.gravityScale = 5;
        Sj2D.enabled = false;
        HookRope.enabled = false;
        IsGrappling = false;
    }

    public void SetGrapplePoint()
    {
        Vector3 difference = new Vector3(InputHandler.MousePos.x, InputHandler.MousePos.y) - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(hookFirePoint.position, difference.normalized, 1 << 11);
        if (hit.collider != null)
        {
            if (Vector2.Distance(hit.point, hookFirePoint.position) <= maxRange && hit.collider.CompareTag("Anclaje") || infiniteRange)
            {
                grapplePoint = hit.point;
                grappleDistanceVector = grapplePoint - (Vector2)transform.position;
                HookRope.enabled = true;
                IsGrappling = true;
                Rb2D.gravityScale = 2;
                AudioClips.PlayHookSound();
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

    #region A1

    /*public void Camo()
    {
        GetComponent<SpriteRenderer>().material = A1Mat;
    }

    public void RmCamo()
    {
        GetComponent<SpriteRenderer>().material = DefMat;
    }*/
    #endregion

    #region A2

    #endregion

    #region A3

    public void StartA3()
    {
        CanGrabEnemies = true;
        Debug.Log("start");
    }

    public void StopA3()
    {
        CanGrabEnemies = false;
        Debug.Log("stop");
    }
    #endregion

    #region A4

    #endregion
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            actualHealth -= 10;
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            collision.gameObject.GetComponent<DamagableEnemies>().TakeDamage(10);
        }
            
    }
    
}
