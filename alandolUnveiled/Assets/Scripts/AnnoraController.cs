using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnnoraController : MonoBehaviour
{
    //Variables Moviemiento
    public float walkspeed = 5f;
    public float runspeed = 10f;
    public float airspeed = 15f;
    public float jumpImpulse = 10f;
    Vector2 moveInput;
    //para saber contra que esta colisionando y actualizar la animacion
    TouchingDirections touchingDirections;

    //Fisicas
    Rigidbody2D rb;
    SpringJoint2D sj;

    //Animacion
    Animator animator;

    public GameObject arm;
    public GameObject sight;
    public bool shot;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        sight.SetActive(false);

        sj = GetComponent<SpringJoint2D>();
        sj.enabled = false;
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

    //Define la posicion del mouse en pantalla
    [SerializeField]
    private Vector2 _mousePos;
    public Vector2 MousePos
    {
        get { return _mousePos; }
        private set
        {
            _mousePos = value;
        }
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
        get { return animator.GetBool(AnimationStrings.canMove); }
    }

    public bool IsAlive
    {
        get { return animator.GetBool(AnimationStrings.isAlive); }
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

        if(IsAiming)
        {
            

            Debug.DrawRay(arm.transform.position, MousePos.normalized, Color.red);
        }
    }


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
            sight.transform.localScale = new Vector3 (1f, 1f, 1f);
            sight.SetActive(true);
        }
        else if (context.canceled)
        {
            IsAiming = false;
            sight.SetActive(false);
        }
    }

    //Funcion que obtiene el input del mouse para apuntar
    public void GetPointerInput(InputAction.CallbackContext context)
    {
        _mousePos = context.ReadValue<Vector2>();
        _mousePos = Camera.main.ScreenToWorldPoint(_mousePos);

        Vector3 difference = new Vector3(_mousePos.x, _mousePos.y) - transform.position;
        difference.Normalize();
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        arm.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + 90);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started && IsAiming)
        {
            shot = true;
        }
        else if (context.canceled)
        {
            shot = false;
            sj.enabled = false;
            rb.gravityScale = 1;
        }
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
