using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    EnemyController enemy;
    public float speed = 10;
    public float dmg = 15;
    public Vector3 launchOffset;
    public bool thrown;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (thrown)
        {
            var direction = transform.right + Vector3.up;
            rb.AddForce(direction * speed, ForceMode2D.Impulse);
        }
        transform.Translate(launchOffset);

        Destroy(gameObject, 5);
    }

    private void FixedUpdate()
    {
        if(!thrown) 
        {
            transform.position += -transform.right * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemy = collision.collider.GetComponent<EnemyController>();
        enemy.GetComponent<Damageable>().Health -= dmg;
        //enemy.healthBar.value -= dmg;

        Debug.Log(collision.gameObject);
    }
}
