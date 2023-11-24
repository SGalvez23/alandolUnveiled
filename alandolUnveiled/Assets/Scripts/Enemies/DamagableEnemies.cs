using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DamagableEnemies : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    //Animator animator;
    //[SerializeField]
    //private float maxHealth = 100;
    private HealthBarEnemies enemyHealthBarEnemies;
    private EnemyController enemyController;

    private void Start()
    {
        currentHealth = maxHealth;
        enemyHealthBarEnemies = GetComponent<HealthBarEnemies>();
        enemyController = GetComponent<EnemyController>();
    }

    private void Update()
    {
        if (currentHealth > maxHealth) { }
    }
    public void TakeDamage(int damage) 
    {

        currentHealth -= damage;

        enemyHealthBarEnemies.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0) 
        {
            enemyController.EnterDeadState();
        }
    }


}
