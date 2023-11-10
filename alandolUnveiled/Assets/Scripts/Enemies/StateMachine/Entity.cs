using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;


    public D_Entity entityData;


    public int facingDirection {get; private set;}

    public Rigidbody2D rb {get; private set;}
    public Animator anim {get; private set;}
    public GameObject enemy {get; private set;}

    private Vector2 velocityWorkspace;

    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform wallCheck;





    public virtual void Start()
  {
        enemy = transform.Find("enemy").gameObject;
        rb = enemy.GetComponent<Rigidbody2D>();
        anim = enemy.GetComponent<Animator>();

        stateMachine = new FiniteStateMachine();
  }

    public virtual void Update()
    {
        Debug.Log("Llego aqui Update entity");
        stateMachine.currentState.UpdateState();
        

    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }


    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkspace;

    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, enemy.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);

    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        enemy.transform.Rotate(0f, 180f, 0f);

    }
}
