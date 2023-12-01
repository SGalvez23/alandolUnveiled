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
            // Hide lever for all clients
            photonView.RPC("HideLever", RpcTarget.All);
            // Call the RPC method to destroy the wall
            photonView.RPC("DestroyWall", RpcTarget.All);

            gameObject.SetActive(false);
            Destroy(wall);
        }
    }

    [PunRPC]
    void DestroyWall()
    {
        // Only on master client, applies to all clients
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(wall);
        }
    }

    [PunRPC]
    void HideLever()
    {
        gameObject.SetActive(false);
    }
}