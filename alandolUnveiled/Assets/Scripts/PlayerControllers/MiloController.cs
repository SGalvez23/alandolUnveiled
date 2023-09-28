using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MiloController : PlayerController
{
    Vector3 throwVector;
    LineRenderer lr;
    public Projectile sarten;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = rb.GetComponent<Animator>();
        touchingDirections = rb.GetComponent<TouchingDirections>();
        lr = GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        if (!LockedVelocity)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

        if (IsAiming)
        {
            MouseOnScreen();
            CalcVecThrow();
            SetArrow();
        }
        else if (!IsAiming)
        {
            RemoveArrow();
        }
    }

    void CalcVecThrow()
    {
        //Vector2 distance = mouseOnScreen - this.transform.position;
        //throwVector = -distance.normalized * 100;
        throwVector = difference;
    }

    void SetArrow()
    {
        lr.positionCount = 2;
        lr.SetPosition(0, firePoint.transform.position);
        //lr.SetPosition(1, throwVector.normalized / 2);
        lr.SetPosition(1, throwVector);
        lr.enabled = true;
    }

    void RemoveArrow()
    {
        lr.enabled = false;
    }

    void ThrowObj()
    {
        //sarten.GetComponent<Rigidbody2D>().AddForce(throwVector);
        Instantiate(sarten, firePoint.position, transform.rotation);
    }

    public void Throw(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            RemoveArrow();
            ThrowObj();
        }
    }
}
