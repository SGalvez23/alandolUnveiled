using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(SpringJoint2D))]
public class PlayerController : MonoBehaviour
{
    //Scripts
    public HookRope hookRope;

    //Variables Moviemiento
    public float walkspeed = 7f;
    public float runspeed = 10f;
    public float airspeed = 12f;
    public float jumpImpulse = 10f;
    Vector2 moveInput;
    //para saber contra que esta colisionando y actualizar la animacion
    TouchingDirections touchingDirections;

    //Fisicas
    SpringJoint2D sj;
    Rigidbody2D rb;

    //Animacion
    Animator animator;

    //Gancho
    public GameObject arm;
    public GameObject sight;
    public bool shot;
    int maxRange = 250;
    Vector3 mouseOnScreen;
    Vector3 difference;
    Vector3 joysitckOnScreen;

    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;
    public Vector2 grapplePoint;
    public Vector2 grappleDistanceVector;
    bool launchToPoint = true;
    float launchSpeed = 0.5f;
    public bool infiniteRange = false;
    private float targetDistance = 3;
    private float targetFrequncy = 1;

    private void Awake()
    {
        sj = GetComponent<SpringJoint2D>();
        sj.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        sight.SetActive(false);
        hookRope.enabled = false;
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

    //Funcion que coloca el punto final del gancho
    void SetGrapplePoint()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, difference.normalized, 1<<11);
        if (hit.collider != null)
        {
            Debug.Log(hit.transform.gameObject.name);
            if (Vector2.Distance(hit.point, firePoint.position) <= maxRange && hit.collider.CompareTag("Anclaje") || infiniteRange)
            {
                grapplePoint = hit.point;
                grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                hookRope.enabled = true;

            }
        }
    }

    //Funcion que modifica el springJoint2D simular el agarre del gancho
    public void Grapple()
    {
        sj.autoConfigureDistance = false;
        if(!launchToPoint && !sj.autoConfigureDistance)
        {
            sj.distance = targetDistance;
            sj.frequency = targetFrequncy;
        }

        if(!launchToPoint)
        {
            if (sj.autoConfigureDistance)
            {
                sj.autoConfigureDistance = true;
                sj.frequency = 0;
            }

            sj.connectedAnchor = grapplePoint;
            sj.enabled = true;
        }
        else
        {
            sj.connectedAnchor = grapplePoint;
            Vector2 distance = firePoint.position - gunHolder.position;
            sj.distance = distance.magnitude;
            sj.frequency = launchSpeed;
            sj.enabled = true;
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

        mouseOnScreen = Camera.main.ScreenToWorldPoint(MousePos);
        difference = new Vector3(mouseOnScreen.x, mouseOnScreen.y) - transform.position;
        Debug.DrawRay(firePoint.position, difference.normalized, Color.yellow);

        if (IsAiming)
        {
            //difference.Normalize();
            //float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            //arm.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ + 90);

            joysitckOnScreen = Camera.main.ScreenToWorldPoint(JoyStickPos);
            Vector3 joyDiff = new Vector3(joysitckOnScreen.x, joysitckOnScreen.y) - transform.position;
            joyDiff.Normalize();
            float rotZ = Mathf.Atan2(joyDiff.y, joyDiff.x) * Mathf.Rad2Deg;
            arm.transform.rotation = Quaternion.Euler(0f, 0, rotZ + 90);
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
            sight.transform.localScale = new Vector3(1, 1, 1);
            sight.SetActive(true);
        }
        else if(context.canceled)
        {
            IsAiming = false;
            sight.SetActive(false);
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

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started && IsAiming)
        {
            shot = true;
            SetGrapplePoint();
            //configurar animacion
        }
        else if (context.canceled)
        {
            shot = false;
            rb.gravityScale = 1;
            sj.enabled = false;
            hookRope.enabled = false;
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
