using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> StateChanged;
    public EnemySpawner eSpawner;
    public int points;

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
    }


    private void Start()
    {
        UpdateGameState(GameState.Gameplay);
        points = 0;
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.Gameplay:
                HandleGameplay();
                break;
            case GameState.Decide:
                HandleDecide();
                break;
            case GameState.Victory:
                HandleVictory();
                break;
        }

        StateChanged?.Invoke(newState);
        Debug.Log(State);
        Debug.Log(points);
    }

    private void HandleGameplay()
    {

    }

    private void HandleDecide()
    {
        if(points > 3)
        {
            UpdateGameState(GameState.Victory);
        }
    }

    private void HandleVictory()
    {
        Loader.Load(Loader.Scene.VictoryScreen);
    }
}

public enum GameState
{
    Gameplay,
    Decide,
    Victory
}