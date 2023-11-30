using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Lever : MonoBehaviourPunCallbacks
{
    public GameObject wall;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BasicAtkHitbox"))
        {
            // Hide lever
            gameObject.SetActive(false);
            // Call the RPC method to destroy the wall
            photonView.RPC("DestroyWall", RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    void DestroyWall()
    {
        // Only the master client will execute this
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(wall);
        }
    }
}