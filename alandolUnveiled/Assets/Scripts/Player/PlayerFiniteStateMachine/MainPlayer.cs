using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MainPlayer : MonoBehaviourPunCallbacks
{
    #region Variables de Estado
    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandedState LandedState { get; private set; }
    public MiloAimState BasicAtkState { get; private set; }
    public Milo_A1State ViejonState { get; private set; }
    public Milo_A2State RojoVivoState { get; private set; }
    public Milo_A3State CheveState { get; private set; }
    public Milo_A4State CarnitaAsadaState { get; private set; }
    #endregion

    #region Componentes
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public LineRenderer LineRenderer { get; private set; }
    public MiloAudioClips AudioClips { get; private set; } 
    PhotonView view;
    #endregion

    #region Check Transforms

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform viejonCheck;
    public Transform leftHand;
    [SerializeField]
    private Transform sartenPrefab;

    #endregion

    #region PlayerData
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDir { get; private set; }

    [SerializeField]
    public PlayerData playerData;

    private Vector2 workspace;
    #endregion

    #region Abilities
    public List<Projectile> projectiles = new List<Projectile>();
    public int ProjectileIndex { get; private set; }
    public GameObject defProjectile;
    public GameObject A1Prefab;
    public bool appliedA1;
    public GameObject A2Prefab;
    public bool AppliedA2 { get; set; }
    public GameObject A3Prefab;
    public bool AppliedA3 { get; set; }
    public int cantA3;
    public GameObject A4Prefab;
    public bool AppliedA4 { get; set; }
    public int cantA4;

    public GameObject sarten;
    public GameObject crosshair;
    private float trajectoryTimeStep = 0.05f;
    private int trajectoryStepCount = 15;
    int projectilesThrown = 0;
    #endregion

    //public Image healthUI;

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "isIdle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "isMoving");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "jump");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandedState = new PlayerLandedState(this, StateMachine, playerData, "landed");
        ViejonState = new Milo_A1State(this, StateMachine, playerData, "viejon");
        BasicAtkState = new MiloAimState(this, StateMachine, playerData, "aiming");
        RojoVivoState = new Milo_A2State(this, StateMachine, playerData, "rojoVivo");
        CheveState = new Milo_A3State(this, StateMachine, playerData, "buff");
        CarnitaAsadaState = new Milo_A4State(this, StateMachine, playerData, "buff");

        FacingDir = 1;
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();
        LineRenderer = GetComponent<LineRenderer>();
        AudioClips = GetComponentInChildren<MiloAudioClips>();

        view = GetComponent<PhotonView>();

        ProjectileIndex = 0;
        ViejonState.ResetA1();
        RojoVivoState.ResetA2();
        CheveState.ResetA3();
        CarnitaAsadaState.ResetA4();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        if (view.IsMine)
        {
            CurrentVelocity = rb.velocity;
            StateMachine.CurrentState.Update();

            if (InputHandler.IsAiming)
            {
                crosshair.transform.position = InputHandler.MouseInput;
                DrawTrajectory();
            }

            if (projectilesThrown > playerData.rojoVivoCant)
            {
                ResetProjectile();
                Debug.Log("se acabou");
            }

            if (projectilesThrown > playerData.cheveCant)
            {
                ResetProjectile();
                Debug.Log("se acabou");
            }

            if (projectilesThrown > playerData.carnitaCant)
            {
                ResetProjectile();
                Debug.Log("se acabou");
            }
        }
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions
    public void SetXVelocity(float velocity)
    {
        workspace.Set(velocity, CurrentVelocity.y);
        rb.velocity = workspace;
        CurrentVelocity = workspace;
    }

    public void SetYVelocity(float velocity)
    {
        workspace.Set(CurrentVelocity.x, velocity);
        rb.velocity = workspace;
        CurrentVelocity = workspace;
    }

    #endregion

    #region Check Functions

    public bool CheckIfGrounded()
    {        
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    public void CheckFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDir)
        {
            Flip();
        }
    }

    public bool CheckAbility1()
    {
        return Physics2D.OverlapCircle(viejonCheck.position, playerData.groundCheckRadius, playerData.whatIsGround);
    }

    #endregion

    #region Other Functions
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();

    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    private void Flip()
    {
        FacingDir *= -1;
        transform.Rotate(0, 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(groundCheck.transform.position, playerData.groundCheckRadius);
    }

    #endregion

    #region BasicAtk
    public void DrawTrajectory()
    {
        Vector3[] positions = new Vector3[trajectoryStepCount];
        for (int i = 0; i < trajectoryStepCount; i++)
        {
            float t = i * trajectoryTimeStep;
            Vector3 pos = (Vector2)leftHand.position + BasicAtkState.Velocity * t + 0.5f * Physics2D.gravity * t;

            positions[i] = pos;
        }

        LineRenderer.positionCount = trajectoryStepCount;
        LineRenderer.SetPositions(positions);
    }

    public void Throw(Vector2 vel, int projectile)
    {
        Projectile throwable = Instantiate(projectiles[projectile], leftHand.position, Quaternion.identity);
        Debug.Log(projectile);
        throwable.GetComponent<Rigidbody2D>().velocity = vel;
        Anim.SetTrigger("basicAtk");
        AudioClips.PlayBasicAtkSound();

        projectilesThrown += 1;
    }

    public void Crosshair()
    {
        crosshair.SetActive(true);
        LineRenderer.enabled = true;
    }

    public void DeleteCrosshair()
    {
        crosshair.SetActive(false);
        LineRenderer.enabled = false;
    }

    public void ResetProjectile()
    {
        ProjectileIndex = 0;
        projectilesThrown = 0;
    }
    #endregion

    #region A1
    public void PlaceViejon()
    {
        GameObject viejon = Instantiate(A1Prefab, viejonCheck.position, Quaternion.identity);
        Destroy(viejon, 4);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("Heal", 1);
    }
    //modificar, no funciona bien el heal
    public void Heal()
    {
        playerData.health += 10;
    }
    #endregion

    #region A2
    public void ApplyA2()
    {
        AppliedA2 = true;
        ProjectileIndex = 1;
    }
    #endregion

    #region A3
    public void CookCheve()
    {
        AppliedA3 = true;
        ProjectileIndex = 2;
    }
    #endregion

    #region A4
    public void CookCarnita()
    {
        AppliedA4 = true;
        ProjectileIndex = 3;
    }
    #endregion
}