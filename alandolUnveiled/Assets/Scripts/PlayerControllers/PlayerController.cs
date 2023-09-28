using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //Variables Moviemiento
    public float walkspeed = 7f;
    public float runspeed = 10f;
    public float airspeed = 12f;
    public float jumpImpulse = 10f;
    protected Vector2 moveInput;
    //para saber contra que esta colisionando y actualizar la animacion
    protected TouchingDirections touchingDirections;

    //Fisicas
    protected Rigidbody2D rb;

    //Animacion
    public Animator animator;

    //Apuntado
    public GameObject arm;
    public Transform firePoint;
    public GameObject sight;
    public bool shot;
    protected int maxRange = 250;
    protected Vector3 mouseOnScreen;
    protected Vector3 difference;
    protected Vector3 joysitckOnScreen;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        sight.SetActive(false);
    }

    //Funcion que define a que velocidad se mueve el personaje
    public float CurrentMoveSpeed
    {
        get
        {
            if (IsMoving && !touchingDirections.IsOnWall && CanMove)
            {
                if (touchingDirections.IsGrounded)
                {
                    if (IsRunning)
                    {
                        return runspeed;
                    }
                    else
                    {
                        return walkspeed;
                    }
                }
                else
                {
                    return airspeed; //Velocidad Aerea, no esta tocando el suelo.
                }
            }
            else { return 0; } //Esta idle
        }
    }
    //Funcion que determina que lado esta mirando en base al input
    private void SetFaceingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void MouseOnScreen()
    {
        mouseOnScreen = Camera.main.ScreenToWorldPoint(MousePos);
        difference = new Vector3(mouseOnScreen.x, mouseOnScreen.y) - transform.position;
        //difference.Normalize();
        float rotationZ = Mathf.Atan2(difference.normalized.y, difference.normalized.x) * Mathf.Rad2Deg;

        if (!IsFacingRight)
        {
            rotationZ += 180;
        }

        arm.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);

        Debug.DrawRay(firePoint.position, difference, Color.red);
    }

    //ESTADOS Y ANIMACIONES
    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    [SerializeField]
    private bool _isRunning = false;
    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    [SerializeField]
    private bool _isAiming = false;
    public bool IsAiming
    {
        get { return _isAiming; }
        private set
        {
            _isAiming = value;
            animator.SetBool(AnimationStrings.isAiming, value);
        }
    }

    //Define la posicion del mouse
    [SerializeField]
    private Vector2 _mousePos;
    public Vector2 MousePos 
    { 
        get { return _mousePos; } 
        private set { _mousePos = value; }
    }

    [SerializeField]
    private Vector2 _joystickPos;
    public Vector2 JoyStickPos
    {
        get { return _joystickPos; }
        private set { _joystickPos = value; }
    }

    [SerializeField]
    private bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                //Para girar el personaje
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    //Definicion del estado del personaje en base al animator
    public bool CanMove
    {
        get { return animator.GetBool(AnimationStrings.canMove);}
    }

    public bool IsAlive
    {
        get{ return animator.GetBool(AnimationStrings.isAlive); }
    }

    public bool LockedVelocity
    {
        get { return animator.GetBool(AnimationStrings.lockVelocity); }
    }

    private void FixedUpdate()
    {
        if (!LockedVelocity)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

        if (IsAiming)
        {
            MouseOnScreen();

            joysitckOnScreen = Camera.main.ScreenToWorldPoint(JoyStickPos);
            Vector3 joyDiff = new Vector3(joysitckOnScreen.x, joysitckOnScreen.y) - transform.position;
            joyDiff.Normalize();
            float rotZ = Mathf.Atan2(joyDiff.y, joyDiff.x) * Mathf.Rad2Deg;
            //arm.transform.rotation = Quaternion.Euler(0f, 0, rotZ + 90);
        }
    }

    //INPUTS
    //Referenciados como Unity Events en el componente de Player Input
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFaceingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsAiming = true;
            //sight.transform.localScale = new Vector3(1, 1, 1);
            //sight.SetActive(true);
        }
        else if(context.canceled)
        {
            IsAiming = false;
            //sight.SetActive(false);
        }
    }

    //Funcion que obtiene el input del mouse para apuntar
    public void GetPointerInput(InputAction.CallbackContext context)
    {
        MousePos = context.ReadValue<Vector2>();
    }

    public void GetJoystickInput(InputAction.CallbackContext context)
    {
        JoyStickPos = context.ReadValue<Vector2>();
        Debug.Log(JoyStickPos);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //to do check if alive
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
        animator.SetTrigger(AnimationStrings.hitTrigger);
    }
}
