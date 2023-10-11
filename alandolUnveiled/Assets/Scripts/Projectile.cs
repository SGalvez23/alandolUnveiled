using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        DOTween.Init();

        if (thrown)
        {
            var direction = transform.right + Vector3.up;
            rb.AddForce(direction * speed, ForceMode2D.Impulse);
        }
        transform.Translate(launchOffset);

        Destroy(gameObject, 5);
        Destroy(GetComponent<Projectile>(), 5);
    }

    private void FixedUpdate()
    {
        if(thrown) 
        {
            transform.position += transform.right * speed * Time.deltaTime;
            transform.DORotate(new Vector3(0, 0, 360), 10, RotateMode.FastBeyond360);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemigo"))
        {
            enemy = collision.collider.GetComponent<EnemyController>();
            enemy.ModifyHealth(dmg);
        }
    }
}
