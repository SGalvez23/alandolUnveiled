using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerRespawner : MonoBehaviourPun
{
    private CheckpointManager checkpointManager;

    private void Awake()
    {
        checkpointManager = FindObjectOfType<CheckpointManager>();
    }

    // Call this method when the player dies
    public void Respawn()
    {
        if (photonView.IsMine && checkpointManager != null)
        {
            StartCoroutine(RespawnAfterDelay(1f)); // Start the coroutine for delayed respawn
        }
    }

    // This coroutine is responsible for the delayed respawn
    private IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Add any additional respawn logic here
        // For example, you might want to reset health or reactivate/deactivate components

        // For simplicity, let's just load the checkpoint data
        checkpointManager.LoadCheckpoint();
    }
}
