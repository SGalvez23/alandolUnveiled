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

    public void SaveCheckpoint()
    {
        /*if (!photonView.IsMine)
            return;*/

        PlayerData data = new PlayerData
        {
            playerX = player.position.x,
            playerY = player.position.y
        };

        string jsonData = JsonUtility.ToJson(data);

        Debug.Log("Guardando Checkpoint...");
        Debug.Log("JSON Data: " + jsonData);
        //photonView.RPC("RPC_SaveCheckpoint", RpcTarget.AllBuffered, jsonData);
    }

    public void LoadCheckpoint()
    {
        if(File.Exists(checkpointFileName))
        {
            string jsonData = File.ReadAllText(checkpointFileName);
            PlayerData data = JsonUtility.FromJson<PlayerData>(jsonData);

            player.position = new Vector2(data.playerX, data.playerY);
        }
    }

    [PunRPC]
    void RPC_SaveCheckpoint(string jsonData)
    {
        File.WriteAllText(checkpointFileName, jsonData);
    }
 
}
