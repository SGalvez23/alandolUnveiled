using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D anyColl;
    AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anyColl = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        audioSource.enabled = false;
        Destroy(gameObject, 3);
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
            collision.gameObject.GetComponent<Damageable>().Health -= 10;
            anyColl.isTrigger = true;
        }
        else if(!collision.gameObject.CompareTag("MainPlayer") & collision.gameObject != null)
        {
            anyColl.isTrigger = true;
            audioSource.enabled = true;
            audioSource.Play();
        }
    }
}
