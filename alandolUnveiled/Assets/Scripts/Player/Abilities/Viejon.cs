using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Viejon : MonoBehaviourPunCallbacks
{
    Annora annora;
    MainPlayer milo;

    private void Start()
    {
        StartCoroutine(DestroyViejon());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Annora"))
        {
            annora = collision.gameObject.GetComponent<Annora>();
            StartCoroutine(HealAnnora());
            Debug.Log("heal annora");
        }
        else if (collision.gameObject.CompareTag("Milo"))
        {
            milo = collision.gameObject.GetComponent<MainPlayer>();
            StartCoroutine(HealMilo());
            Debug.Log("heal milo");
        }
    }

    IEnumerator HealAnnora()
    {
        yield return new WaitForSeconds(2f);
        annora.actualHealth += 32f;
    }

    IEnumerator HealMilo()
    {
        yield return new WaitForSeconds(2f);
        milo.actualHealth += 32f;
    }

    IEnumerator DestroyViejon()
    {
        yield return new WaitForSeconds(6f);
        photonView.RPC("DestroyPrefab", RpcTarget.All);
    }

    [PunRPC]
    void DestroyPrefab()
    {
        Destroy(gameObject);
    }
}
