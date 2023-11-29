using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Lever : MonoBehaviour
{
    public GameObject wall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BasicAtkHitbox"))
        {
            // Hide lever on 
            gameObject.SetActive(false);
            // Check if this is the master client before destroying the wall
            if (PhotonNetwork.IsMasterClient)
            {
                // Use PhotonNetwork.Destroy to ensure the destruction is synchronized
                PhotonNetwork.Destroy(wall);
            }
        }
    }
}
