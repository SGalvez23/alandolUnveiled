using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
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
    #endregion

    #region Componentes
    public Animator Anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    #region Check Transforms

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform viejonCheck;
    [SerializeField]
    private Transform leftHand;

    #endregion

    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDir { get; private set; }

    [SerializeField]
    private PlayerData playerData;

    private Vector2 workspace;

    public GameObject viejon;
    public bool placed;

    public Projectile sarten;
    public GameObject crosshair;

    public Image healthUI;

    #region Unity Callback Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, playerData, "isIdle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "isMoving");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandedState = new PlayerLandedState(this, StateMachine, playerData, "landed");
        ViejonState = new Milo_A1State(this, StateMachine, playerData, "viejon");
        BasicAtkState = new MiloAimState(this, StateMachine, playerData, "aiming");

        FacingDir = 1;
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        rb = GetComponent<Rigidbody2D>();

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        CurrentVelocity = rb.velocity;
        StateMachine.CurrentState.Update();

        if (Input.GetKeyDown(KeyCode.F))
        {
            playerData.health -= 20;
        }

        healthUI.fillAmount = playerData.health / 100f;

        if (InputHandler.IsAiming)
        {
            crosshair.transform.position = InputHandler.MouseInput;
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

    public void PlaceViejon()
    {
        Instantiate(viejon, viejonCheck.position, Quaternion.identity);
        placed = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("Heal", 1);
    }

    public void Heal()
    {
        playerData.health += 10;
    }

    public void MiloBasicAtk()
    {
        Projectile s = Instantiate(sarten, leftHand.position, Quaternion.identity);
        Anim.SetTrigger("basicAtk");
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