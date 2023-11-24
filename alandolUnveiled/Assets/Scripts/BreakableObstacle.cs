using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Sarten") || collision.gameObject.CompareTag("BasicAtkHitbox"))
        {
            Destroy(gameObject);
        }
    }
}
