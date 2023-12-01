using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class EnemyController : MonoBehaviourPunCallbacks, IPunObservable
{
    EnemySpawner enemySpawner;


    // ............ States.....
    private enum State
    {

        Walking,
        Attack,
        Jump,
        Dead

    }

    private State currentState;


    // Variables ........

    [SerializeField]
    private float groundCheckDistance, wallCheckDistance, movementSpeed;
    [SerializeField]
    private Transform groundCheck, wallCheck, attackCheck;

    [SerializeField]
    private LayerMask whatIsGround, whatIsPlayer;

    // Define una variable para el jugador (suponiendo que tienes una referencia al GameObject del jugador)
    [SerializeField]
    private Transform player;

    // Define la distancia a la que el enemigo detectará y atacará al jugador
    [SerializeField]
    private float attackRange = 2f;



    // Define el tiempo entre ataques
    [SerializeField]
    private float attackRate = 1f;
    private float nextAttackTime = 0f;




    private bool groundDetected, wallDetected, playerDetected;

    private int facingDir;

    private float currentHealth;
    public Slider health;

    private Vector2 movement;

    public float damageHopSpeed;

    private Rigidbody2D enemyRb;
    private Animator enemyAnim;


    [SerializeField]
    GameObject basicHitbox;
    AttackDetails attackDetails;
    int lastDamageDirection;


    //.....  basic Functions ........
    private void Start()
    {
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>(); // Obtener la referencia al EnemySpawner

        enemyRb = GetComponent<Rigidbody2D>();
        enemyAnim = GetComponent<Animator>();

        currentHealth = 70f;
        damageHopSpeed = 10f;
        facingDir = 1;
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer, true);
    }


    private void Update()
    {
        // Debug.Log(playerDetected);
        if (photonView.IsMine)
        {
            health.value = currentHealth / 70;

            if(currentHealth <= 0)
            {
                Destroy(gameObject);
            }

            switch (currentState)
            {
                case State.Walking:
                    UpdateWalkingState();
                    break;
                case State.Attack:
                    UpdateAttackState();
                    break;
                case State.Dead:
                    UpdateDeadState();
                    break;
            }
        }

    }

    #region Walking State
    // .................. Walking State ....................................
    private void EnterWalkingState()
    {

    }

    private void UpdateWalkingState()
    {

        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        playerDetected = Physics2D.Raycast(attackCheck.position, transform.right, attackRange, whatIsPlayer);

        if (!groundDetected || wallDetected)
        {
            Flip();

        }
        else if (playerDetected)
        {
            SwicthState(State.Attack);
        }
        else
        {
            movement.Set(movementSpeed * facingDir, enemyRb.velocity.y);
            enemyRb.velocity = movement;
            enemyAnim.SetBool("canWalk", true);
        }


    }

    private void ExitWalkingState()
    {
        enemyAnim.SetBool("canWalk", false);
    }

    #endregion

    #region Dead State

    // .................. Dead State ....................................
    public void EnterDeadState()
    {
        // spawn death animation and particles
        //Play sound dead
        EnemyDefeated();
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    #endregion

    #region Attack State

    // .................. Attack State ....................................

    private void EnterAttackState()
    {
        enemyAnim.SetBool("Attack", true);
        if (!playerDetected)
        {
            SwicthState(State.Walking);
        }
    }

    private void UpdateAttackState()
    {

        if (!playerDetected)
        {
            SwicthState(State.Walking);
        }
    }

    private void ExitAttackState()
    {
        enemyAnim.SetBool("Attack", false);
    }


    #endregion
    // ....... Other Functions ............................................


    private void Flip()
    {
        facingDir *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void SwicthState(State state)
    {

        switch (currentState)
        {
            case State.Walking:
                ExitWalkingState();
                break;
            case State.Attack:
                ExitAttackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Walking:
                EnterWalkingState();
                break;
            case State.Attack:
                EnterAttackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Writing data to send over the network
            stream.SendNext(transform.position);

        }
        else
        {
            // Reading data received from the network
            transform.position = (Vector3)stream.ReceiveNext();


        }
    }

    private void EnemyDefeated()
    {
        if (enemySpawner != null)
        {
            enemySpawner.EnemyDefeated();
            PhotonNetwork.Destroy(gameObject);
            // Llamar a la función EnemyDefeated del EnemySpawner
        }
    }
    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        collision.gameObject.GetComponent<Annora>().actualHealth -= 10;
    //    }
    //}

    public void CheckAttackHitBox()
    {
        Collider2D[] detectedObjs = Physics2D.OverlapCircleAll(basicHitbox.transform.position, 2f, whatIsPlayer);

        attackDetails.damageAmount = 8f;
        attackDetails.position = transform.position;

        foreach (Collider2D collider in detectedObjs)
        {
            collider.transform.SendMessage("Damage", attackDetails);
        }
    }

    public virtual void DamageHop(float velocity)
    {
        movement.Set(velocity, velocity * 0.45f);
        enemyRb.velocity = movement;
    }

    public virtual void Damage(AttackDetails attackDetails)
    {
        currentHealth -= attackDetails.damageAmount;

        DamageHop(damageHopSpeed);

        if (attackDetails.position.x > transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));

        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));

        Gizmos.DrawCube(basicHitbox.gameObject.transform.position, new Vector3(1,1,1));
    }
}