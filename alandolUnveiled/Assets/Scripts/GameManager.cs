using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int amountEnemies;
    private PlayerData playerData;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Mas de un gameManager");
        }

        amountEnemies = 3;
        
    }

    private void Update()
    {
     
     Lose();
     Win();
     
    
    }

    private void Lose(){
         if(playerData.health == 0)
        {
           SceneManager.LoadScene("SampleScene");
        }
    

    }
    private void Win()
    {
           if(amountEnemies == 0)
        {
           SceneManager.LoadScene("VictoryScreen");
        }

    }

    
}

