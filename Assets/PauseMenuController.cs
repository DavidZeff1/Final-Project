using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;  
    [SerializeField] private GameStateChannel gameStateChannel;
    [SerializeField] private StateSO pauseStateSO;  

    private void Start()
    {
        gameStateChannel.StateEnter += OnStateEnter;
        gameStateChannel.StateExit += OnStateExit;
        pausePanel.SetActive(false);
    }

    private void OnStateEnter(GameState state)
    {
        if (state.stateSO == pauseStateSO)
        {
            pausePanel.SetActive(true);
        }
    }

    private void OnStateExit(GameState state)
    {
        if (state.stateSO == pauseStateSO)
        {
            pausePanel.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        gameStateChannel.StateEnter -= OnStateEnter;
        gameStateChannel.StateExit -= OnStateExit;
    }
}

