using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Cinemachine;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    public GameObject[] playerPrefabs;
    public Transform[] spawnPoints;
    public CinemachineVirtualCamera VC;

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            int randomNumber = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomNumber];
            GameObject playerToSpawn = playerPrefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["playerAvatar"]];
            GameObject newPlayer = PhotonNetwork.Instantiate(playerToSpawn.name, spawnPoint.position, Quaternion.identity);
            CinemachineVirtualCamera playerVC = Instantiate(VC, spawnPoint.position, Quaternion.identity);
            playerVC.Follow = newPlayer.transform;
        }
        else
        {
            int randomNumber = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomNumber];
            GameObject playerToSpawn = playerPrefabs[0];
            GameObject newPlayer = Instantiate(playerToSpawn, spawnPoint.position, Quaternion.identity);
            CinemachineVirtualCamera playerVC = Instantiate(VC, spawnPoint.position, Quaternion.identity);
            playerVC.Follow = newPlayer.transform;
        }
    }
}
