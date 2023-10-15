using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyController enemy;

    private void Awake()
    {
        GameManager.StateChanged += GMOnStateChange;
    }

    private void OnDestroy()
    {
        GameManager.StateChanged -= GMOnStateChange;
    }

    private void GMOnStateChange(GameState state)
    {
        if(state == GameState.Gameplay)
        {
            StartCoroutine(Spawner());
        }
    }

    IEnumerator Spawner()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(3);
            Instantiate(enemy, gameObject.transform);
        }

        Destroy(this);
        GameManager.Instance.UpdateGameState(GameState.Decide);
    }
}
