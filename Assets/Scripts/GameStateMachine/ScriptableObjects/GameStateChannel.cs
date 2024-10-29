using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game State Channel", menuName = "Channels/Game State")]
public class GameStateChannel : ScriptableObject
{
    public Action<GameState> StateEnter;
    public Action<GameState> StateExit;
    public Func<GameState> GetCurrentState;
    
    public void StateEntered(GameState gameState)
    {
        //all functions that are listening to "StateEnter" event will be triggered
        //(ex: gameStateChannel.StateEnter += OnStateEnter; in GameStateListener script)
        StateEnter?.Invoke(gameState);
    }

    public void StateExited(GameState gameState)
    {
        StateExit?.Invoke(gameState);
    }

    public GameState GetCurrentGameState()
    {
        return GetCurrentState?.Invoke();
    }
}
