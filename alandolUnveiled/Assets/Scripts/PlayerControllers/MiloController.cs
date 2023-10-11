using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiloController : PlayerController
{
    public Projectile sarten;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = rb.GetComponent<Animator>();
        touchingDirections = rb.GetComponent<TouchingDirections>();
    }

    private void FixedUpdate()
    {
        if (!LockedVelocity)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

        if (IsAiming)
        {
            MouseOnScreen();
        }
    }

    void ThrowObj()
    {
        Instantiate(sarten, firePoint.position, transform.rotation);
    }

    public void Throw(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ThrowObj();
        }
    }
}
