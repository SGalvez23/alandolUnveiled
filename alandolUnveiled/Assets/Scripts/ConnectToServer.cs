using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    //Conectar a Photon Server
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    //Estamos conectados a photon y ahora se ingresa al lobby
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    //aqui se carga el lobby
    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Room");
    }
    
}
