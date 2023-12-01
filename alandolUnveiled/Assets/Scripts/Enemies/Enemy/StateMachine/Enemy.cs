using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public Data_Enemy enemyData;
    public AttackState attackState;

    public int facingDir {  get; private set; }
    public Rigidbody2D Rb2D { get; private set; }
    public Animator Anim { get; private set; }
    public AudioSource AudioSource { get; private set; }

    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerCheck;

    [SerializeField]
    private float currentHealth;
    private float lastDamageTime;
    private int lastDamageDirection;

    private Vector2 velocityWorkspace;

    protected bool isDead;

    public virtual void Start()
    {
        Anim = GetComponent<Animator>();
        Rb2D = GetComponent<Rigidbody2D>();
        AudioSource = GetComponent<AudioSource>();

        AudioSource.enabled = false;
        currentHealth = enemyData.maxHealth;
        facingDir = 1;

        int enemyLayer = LayerMask.NameToLayer("Enemy");
        Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer, true);

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDir * velocity, Rb2D.velocity.y);
        Rb2D.velocity = velocityWorkspace;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, transform.right, enemyData.wallCheckDistance, enemyData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, enemyData.ledgeCheckDistance, enemyData.whatIsGround);
    }

    public virtual bool CheckPlayerInMinAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, enemyData.minAggroDistance, enemyData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInMaxAggroRange()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, enemyData.maxAggroDistance, enemyData.whatIsPlayer);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, transform.right, enemyData.closeRangeActionDistance, enemyData.whatIsPlayer);
    }

    public virtual void DamageHop(float velocity)
    {
        velocityWorkspace.Set(velocity, velocity / 3);
        Rb2D.velocity = velocityWorkspace;
    }

    public virtual void Damage(AttackDetails attackDetails)
    {
        currentHealth -= attackDetails.damageAmount;

        DamageHop(enemyData.damageHopSpeed);
        PlaySound();

        if(attackDetails.position.x > transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else
        {
            lastDamageDirection = 1;
        }

        if(currentHealth <= 0)
        {
            isDead = true;
        }
    }

    public virtual void Death()
    {
        Destroy(gameObject);
    }

    public void PlaySound()
    {
        AudioSource.clip = enemyData.damagedSound;
        AudioSource.enabled = true;
        AudioSource.Play();
    }

    public virtual void Flip()
    {
        facingDir *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

    public void AnimationTrigger() => attackState.TriggerAttack();

    public void AnimationFinishTrigger() => attackState.FinishAttack();

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDir * enemyData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * enemyData.ledgeCheckDistance));
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(Vector2.right * facingDir * enemyData.minAggroDistance));
    }
}
