using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConradController : PlayerController
{
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = rb.GetComponent<Animator>();
        touchingDirections = rb.GetComponent<TouchingDirections>();
        //sight.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!LockedVelocity)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }
}
