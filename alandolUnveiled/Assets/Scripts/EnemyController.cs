using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class EnemyController : MonoBehaviour
{
    Rigidbody2D rb;
    TouchingDirections touchingDirections;
    Animator animator;

    public float walkspeed = 3f;
    public float walkStopRate = 0.05f;
    public DetectionZone attackZone;

    public enum WalkableDir { Right, Left };
    private WalkableDir _walkDir;
    private Vector2 walkDirVector;

    public WalkableDir WalkDir
    {
        get { return _walkDir; }
        set
        {
            if(_walkDir != value )
            {
                //Voltear al enemigo
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if(value == WalkableDir.Right )
                {
                    walkDirVector = Vector2.right;
                }
                else if(value == WalkableDir.Left)
                {
                    walkDirVector = Vector2.left;
                }
            }

            _walkDir = value;
        }
    }

    public bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public bool CanMove
    {
        get 
        { 
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (touchingDirections.IsGrounded && touchingDirections.IsOnWall)
        {
            FlipDirection();
        }

        if(CanMove)
            rb.velocity = new Vector2(walkspeed * walkDirVector.x, rb.velocity.y);
        else
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
    }

    private void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
    }

    private void FlipDirection()
    {
        if(WalkDir == WalkableDir.Right )
        {
            WalkDir = WalkableDir.Left;
        }
        else if(WalkDir == WalkableDir.Left)
        {
            WalkDir = WalkableDir.Right;
        }
        else
        {
            Debug.LogError("error");
        }
    }
}
