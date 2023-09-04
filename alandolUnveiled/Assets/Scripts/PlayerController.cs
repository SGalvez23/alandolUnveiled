using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float walkspeed = 5f;
    public float runspeed = 10f;
    public float airspeed = 15f;
    public float jumpImpulse = 10f;
    Vector2 moveInput;
    TouchingDirections touchingDirections;

    public GameObject arm;
    public GameObject sight;

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
                    //air movement
                    return airspeed;
                }
            }
            //is idle
            else { return 0; }
        }
    }

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

    public bool _isFacingRight = true;
    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    public bool LockedVelocity
    {
        get { return animator.GetBool(AnimationStrings.lockVelocity); }
    }

    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        sight.transform.localScale = new Vector3(0, 0, 0);
    }

    private void FixedUpdate()
    {
        if (!LockedVelocity)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

        
    }

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

    private void SetFaceingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            //face right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            //face left
            IsFacingRight = false;
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
        }
        else if(context.canceled)
        {
            IsAiming= false;
            sight.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    public void GetPointerInput(InputAction.CallbackContext context)
    {
        if(IsAiming)
        {
            Vector3 mousePos = context.ReadValue<Vector2>();
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 cam = Camera.main.ScreenToWorldPoint(mousePos);

            Vector3 difference = cam - transform.position;
            difference.Normalize();
            float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            arm.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //todo check if alive
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
    }
}
