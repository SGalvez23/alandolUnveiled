using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyController enemy;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(2);
            Instantiate(enemy, gameObject.transform);
        }
    }
}   
