using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Damageable : MonoBehaviour
{
    public UnityEvent<float, Vector2> damagableHit;
    Animator animator;
    [SerializeField]
    private float _maxHealth = 100;
    public Annora annora;

    public float MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    [SerializeField]
    private float _health = 100;
    public float Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;
    [SerializeField]
    private bool isInvincible = false;

    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    public bool IsAlive
    {
        get
        { return _isAlive; }
        private set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set " + value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }

        if (!IsAlive)
        {
            Destroy(gameObject);
            GameManager.Instance.amountEnemies -= 1;
        }
    }

    public bool Hit(float damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            animator.SetTrigger(AnimationStrings.hitTrigger);
            damagableHit?.Invoke(damage, knockback);
            //healthBar.fillAmount = _health / 100f;

            return true;
        }

        return false;
    }
    //Es el que hace daño al jugador
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            Debug.Log("Player is hurt!");
        }
        if (collision.gameObject.tag == "Enemigo")
        {

            OnDamaged(collision.transform.position);
        }
    }
    void OnDamaged(Vector2 targetPos)
    {
        //Layer es del player
        //gameObject.layer == 2;
        if (annora.annoraData.health <= 0)
        {
            Debug.Log("DEAD");
        }
        annora.annoraData.health -= 10;
        //lo que pasara
        //annora.spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // Reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        annora.Rb2D.AddForce(new Vector2(dirc, 1) * 10, ForceMode2D.Impulse);

        Invoke("OffDamage", 3);
        OffDamage();
    }
    void OffDamage()
    {

       //gameObject.layer = 10;
        //spriteRenderer.color = new Color(1, 1, 1, 1);

    }
}

