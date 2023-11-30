using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viejon : MonoBehaviour
{
    CapsuleCollider2D capsule;    

    void Start()
    {
        capsule = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MainPlayer"))
        {
            Debug.Log("heal");
        }
    }
}
