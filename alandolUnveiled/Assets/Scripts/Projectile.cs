using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = false;
        Destroy(gameObject, 3);
        Physics2D.IgnoreLayerCollision(gameObject.layer, 7, true);
    }

    private void Update()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            collision.gameObject.GetComponent<DamagableEnemies>().TakeDamage(10);
            Destroy(gameObject);
        }
        else if(!collision.gameObject.CompareTag("MainPlayer") & collision.gameObject != null)
        {
            audioSource.enabled = true;
            audioSource.Play();
            Destroy(gameObject, 1);
        }
    }
}
