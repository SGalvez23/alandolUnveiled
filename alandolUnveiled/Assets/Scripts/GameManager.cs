using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int amountEnemies;
    //AjusteEntreEscenas ajustes;
    public CinemachineVirtualCamera virtualCamera;

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

        amountEnemies = 4;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if(amountEnemies == 0)
        {
            //Loader.Load(Loader.Scene.VictoryScreen);
        }
    }
}

