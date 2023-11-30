using System.Collections;
using UnityEngine;
using Photon.Pun;
public class EnemySpawner : MonoBehaviourPunCallbacks
{
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 2f;
    public int enemiesPerWave = 5;
    private int playersInside = 0;
    private bool waveStarted = false;

    private int totalEnemiesInWave = 15;
    private int enemiesDefeated = 0;
    public GameObject wall;
    [PunRPC]
    void RPC_CloseRoom()
    {
        wall.SetActive(true);
        Debug.Log("Room closed!");
    }
    [PunRPC]
    void RPC_OpenRoom()
    {
        // L�gica para abrir la habitaci�n despu�s de que se maten todos los enemigos
        // Puedes activar ciertos elementos o cambiar propiedades seg�n tus necesidades
        wall.SetActive(false);

        Debug.Log("Room opened!");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playersInside++;
            if (playersInside == PhotonNetwork.CurrentRoom.PlayerCount && !waveStarted && PhotonNetwork.IsMasterClient)
            {
                photonView.RPC("RPC_CloseRoom", RpcTarget.AllBuffered);
                StartEnemyWave();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playersInside--;
            if (!waveStarted)
            {
                StopEnemyWave();
            }
        }
    }
    void StartEnemyWave()
    {
        waveStarted = true;
        photonView.RPC("RPC_StartEnemyWave", RpcTarget.AllBuffered);
        totalEnemiesInWave = enemiesPerWave;
        enemiesDefeated = 0;
        StartCoroutine(SpawnEnemies());
    }
    [PunRPC]
    void RPC_StartEnemyWave()
    {
        waveStarted = true;
    }
    void StopEnemyWave()
    {
        waveStarted = false;
        // L�gica para detener la oleada si los jugadores salen del �rea antes de que comience
    }
    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < enemiesPerWave; i++)
        {
            int randomPrefabIndex = Random.Range(0, enemyPrefabs.Length);
            int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
            GameObject enemy = PhotonNetwork.Instantiate(enemyPrefabs[randomPrefabIndex].name, spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }


    public void EnemyDefeated()
    {
        enemiesDefeated++;
        if (enemiesDefeated >= totalEnemiesInWave)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                photonView.RPC("RPC_AllEnemiesDefeated", RpcTarget.All);
            }
            //photonView.RPC("RPC_OpenRoom", RpcTarget.AllBuffered);
        }
    }


    [PunRPC]
    void RPC_AllEnemiesDefeated()
    {
        // Lógica para terminar el juego cuando todos los enemigos hayan sido derrotados
        // Por ejemplo, podrías llamar a una función que maneje el final del juego
        PhotonNetwork.LoadLevel("VictoryScreen");
    }
}