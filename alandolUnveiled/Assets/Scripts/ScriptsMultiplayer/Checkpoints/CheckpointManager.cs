using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class CheckpointManager : MonoBehaviourPun
{
    [System.Serializable]
    public class PlayerData
    {
        public float playerX;
        public float playerY;
    }

    public Transform player;
    public string checkpointFileName = "checkpoint.json";
    string path;

    private void Start()
    {
        path = Path.Combine(Application.persistentDataPath, checkpointFileName);
    }

    public void GetPlayer(GameObject playerTransform)
    {
        player = playerTransform.transform;
    }

    public void SaveCheckpoint()
    {
        if (photonView.IsMine)
        {
            PlayerData data = new PlayerData
            {
                playerX = player.transform.position.x,
                playerY = player.transform.position.y
            };

            string jsonData = JsonUtility.ToJson(data);

            photonView.RPC("RPC_SaveCheckpoint", RpcTarget.AllBuffered, jsonData);
        }
    }

    public void LoadCheckpoint()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("RPC_LoadCheckpoint", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void RPC_SaveCheckpoint(string jsonData)
    {
        File.WriteAllText(path, jsonData);
        Debug.Log("save" + jsonData);
    }

    [PunRPC]
    void RPC_LoadCheckpoint()
    {
        string jsonData = File.ReadAllText(path);
        PlayerData data = JsonUtility.FromJson<PlayerData>(jsonData);

        player.position = new Vector2(data.playerX, data.playerY);
        Debug.Log("load" + jsonData);
    }
 
}
