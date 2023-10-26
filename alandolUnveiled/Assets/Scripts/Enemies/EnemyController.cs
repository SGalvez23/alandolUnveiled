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
    private float groundCheckDistance, wallCheckDistance, movementSpeed, maxHealth,  knockbackDuration, lastTouchDamageTime, touchDamageCooldown, touchDamage,
    touchDamageWidth, touchDamageHeight;

    [SerializeField]
    private Transform groundCheck, wallCheck, touchDamageCheck;

    [SerializeField]
    private LayerMask whatIsGround, whatIsPlayer;

    [SerializeField]
    private PlayerData playerData;


    public MainPlayer playerHealth;
    [SerializeField]
    private Vector2 knockbackSpeed;


    private bool groundDetected, wallDetected;

    private int facingDir, damageDirection;

    private float currentHealth, knockbackStartTime;
    
    private Vector2 movement, touchDamageBotLeft, touchDamageTopRight;

    private GameObject enemy;
    private Rigidbody2D enemyRb;
    private Animator enemyAnim;
    

    private float[] attackDetails = new float[2];


    //.....  basic Functions ........
    private void Start()
    {
        enemy = transform.Find("enemy").gameObject;
        enemyRb = enemy.GetComponent<Rigidbody2D>();  
        enemyAnim = enemy.GetComponent<Animator>();
        facingDir = 1;
        currentHealth = maxHealth;
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
            case State.Knockback:
                UpdateKnockbackState();
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


    // .................. Knockback State ....................................



    private void EnterKnockbackState()
    {
        knockbackStartTime = Time.time; 
        movement.Set(knockbackSpeed.x * damageDirection, knockbackSpeed.y);
        enemyRb.velocity = movement;
        enemyAnim.SetBool("Knockback", true);
    }
    
    private void UpdateKnockbackState()
    {
        if (Time.time >= knockbackDuration * knockbackStartTime)
        {
            SwicthState(State.Walking);
        }
    }

    private void ExitKnockbackState()
    {
        enemyAnim.SetBool("Knockback", false);
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




    public void Damage(float dmg)
    {
        currentHealth -= dmg;
        Debug.Log("Hago daño");
        Debug.Log(currentHealth);
        if (dmg > enemy.transform.position.x)
        {
            damageDirection = -1;
        }
        else{
            damageDirection = 1;
        }
        
        //Hit Particles

        if (currentHealth > 0.0f)
        {
            SwicthState(State.Knockback);

        }
        else if (currentHealth <= 0.0f)
        {
            SwicthState(State.Dead);
            //Die
        }
        
    }

    private void CheckTouchDamage()
    {
        if (Time.time >= lastTouchDamageTime + touchDamageCooldown)
        {
            touchDamageBotLeft.Set( touchDamageCheck.position.x -(touchDamageWidth / 2  ), touchDamageCheck.position.y -(touchDamageHeight/ 2));
            touchDamageTopRight.Set( touchDamageCheck.position.x +(touchDamageWidth / 2  ), touchDamageCheck.position.y +(touchDamageHeight/ 2));
        
            Collider2D hit  = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);

            if (hit != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = enemy.transform.position.x;
                
            }
        
        
        }
    }

    private void Flip()
    {
        facingDir *= -1;
        enemy.transform.Rotate(0.0f, 180.0f, 0.0f);

    }




    private void SwicthState(State state)
    {

        switch(currentState)
        {
            case State.Walking:
                ExitWalkingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
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
            case State.Knockback:
                EnterKnockbackState();
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
