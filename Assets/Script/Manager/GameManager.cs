using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;

    public static event Action<GameState> OnGameStateChange;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);    
    }

    public void UpdateGameState(GameState newState)
    {
        State = newState;

        switch (newState)
        {
            case GameState.state1:
                function1();
                break;
            case GameState.state2:
                break;
            case GameState.state3:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChange?.Invoke(newState);
    }

    void Start()
    {
        var findObjects = FindObjectOfType<DynamicBone>();
        UpdateGameState(GameState.state1);
    }

    void function1()
    {

    }
}

public enum GameState
{
    state1,
    state2,
    state3,
}
