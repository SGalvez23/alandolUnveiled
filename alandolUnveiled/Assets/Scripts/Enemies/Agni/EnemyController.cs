using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyController : MonoBehaviour
{

    // ............ States.....
    private enum State{

        Walking,
        Knockback,
        Dead

    }

    private State currentState;


    // Variables ........

    [SerializeField]
    private float groundCheckDistance, wallCheckDistance, movementSpeed;
    [SerializeField]
    private Transform groundCheck, wallCheck;

    [SerializeField]
    private LayerMask whatIsGround;

    private bool groundDetected, wallDetected;

    private int facingDir, damageDirection;

    private float currentHealth;
    
    private Vector2 movement, touchDamageBotLeft, touchDamageTopRight;

    private GameObject enemy;
    private Rigidbody2D enemyRb;
    private Animator enemyAnim;


    //.....  basic Functions ........
    private void Start()
    {
        //enemy = transform.Find("enemy").gameObject;
        enemyRb = GetComponent<Rigidbody2D>();  
        enemyAnim = GetComponent<Animator>();

        facingDir = 1;
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        Physics.IgnoreLayerCollision(enemyLayer, enemyLayer, true);
}


    private void Update()
    {

        switch(currentState)
        {
            case State.Walking:
                UpdateWalkingState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }



    }

    // .................. Walking State ....................................
    private void EnterWalkingState()
    {

    }
    
    private void UpdateWalkingState()
    {

        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        
        wallDetected = Physics2D.Raycast(wallCheck.position,transform.right, wallCheckDistance, whatIsGround);

        if (!groundDetected || wallDetected)
        {   
            Flip();
            
            

        }
        else 
        {
            movement.Set(movementSpeed * facingDir, enemyRb.velocity.y);
            enemyRb.velocity = movement;
                
        }
    }

    private void ExitWalkingState()
    {

    }

    // .................. Dead State ....................................
    private void EnterDeadState()
    {
        // spawn death animation and particles
        Destroy(gameObject);
    }
    
    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }



    // ....... Other Functions ............................................




    

    private void Flip()
    {
        facingDir *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);

    }




    private void SwicthState(State state)
    {

        switch(currentState)
        {
            case State.Walking:
                ExitWalkingState();
                break; 
            case State.Dead:
                ExitDeadState();
                break;   
        }


        switch(state)
        {
            case State.Walking:
                EnterWalkingState();
                break; 
            case State.Dead:
                EnterDeadState();
                break;   
        }


        currentState = state;

        
    }

  
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}
