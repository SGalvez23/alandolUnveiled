using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BreakableObstacle : MonoBehaviourPunCallbacks
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BasicAtkHitbox"))
        {
            // Call the RPC method to destroy the obstacle on all clients
            photonView.RPC("DestroyObstacle", RpcTarget.All);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Sarten"))
        {
            // Call the RPC method to destroy the obstacle on all clients
            photonView.RPC("DestroyObstacle", RpcTarget.All);
        }
    }

    [PunRPC]
    void DestroyObstacle()
    {
        // This will be executed on all clients
        Destroy(gameObject);
    }
}