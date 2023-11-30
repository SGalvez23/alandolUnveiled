/*
using System.Collections;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviourPunCallbacks
{
    public enum SpawnerState
    {
        Inactive,
        Opening,
        Spawning,
        Closed
    }
    public float openingTime = 2f;
    public GameObject[] enemyPrefabs;
    public Transform[] spawnPoints;
    private int playersInside = 0;
    [SerializeField] private SpawnerState currentState = SpawnerState.Inactive;

 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playersInside++;

            if (currentState == SpawnerState.Inactive && playersInside == PhotonNetwork.CurrentRoom.PlayerCount)
            {
                photonView.RPC("ActivateSpawner", RpcTarget.All);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playersInside--;

            if (currentState == SpawnerState.Spawning)
            {
                currentState = SpawnerState.Inactive;
            }
        }
    }

    [PunRPC]
    private void ActivateSpawner()
    {
        Debug.Log("LLega");
        StartCoroutine(ActivateSpawnerCoroutine());
    }

    IEnumerator ActivateSpawnerCoroutine()
    {
        currentState = SpawnerState.Opening;

        yield return new WaitForSeconds(openingTime);

        currentState = SpawnerState.Spawning;
        photonView.RPC("SpawnEnemies", RpcTarget.All);

        yield return new WaitWhile(() => AllEnemiesSpawned());

        currentState = SpawnerState.Closed;
    }

    private bool AllEnemiesSpawned()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemigo");
        return enemies.Length >= enemyPrefabs.Length;
    }
    [PunRPC]
    private void SpawnEnemies()
    {
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    IEnumerator SpawnEnemiesCoroutine()
    {
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            int randomPrefabIndex = Random.Range(0, enemyPrefabs.Length);
            int randomSpawnPointIndex = Random.Range(0, spawnPoints.Length);
            GameObject enemy = PhotonNetwork.Instantiate(enemyPrefabs[randomPrefabIndex].name, spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);
            yield return new WaitForSeconds(2f); // Adjust this delay as needed
        }

        currentState = SpawnerState.Closed;
    }
}
*/

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

    [PunRPC]
    void RPC_CloseRoom()
    {
        // L�gica para cerrar la habitaci�n cuando se inicia la oleada de enemigos
        // Puedes desactivar ciertos elementos o cambiar propiedades seg�n tus necesidades
        Debug.Log("Room closed!");
    }

    [PunRPC]
    void RPC_OpenRoom()
    {
        // L�gica para abrir la habitaci�n despu�s de que se maten todos los enemigos
        // Puedes activar ciertos elementos o cambiar propiedades seg�n tus necesidades
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

        photonView.RPC("RPC_OpenRoom", RpcTarget.AllBuffered);
    }
}