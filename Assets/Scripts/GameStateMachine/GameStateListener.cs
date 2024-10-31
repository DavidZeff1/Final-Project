using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateListener : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    public AudioClip normalMusic;
    public AudioClip nightmareMusic;

    private GameStateChannel gameStateChannel;

    private void Start()
    {
        var beacon = FindObjectOfType<Beacon>();
        gameStateChannel = beacon.gameStateChannel;
        //add function "OnStateEnter" to event "StateEnter", when a stateEnter is triggered then
        //"OnStateEnter" will be activated with the gamestate as a parameter
        gameStateChannel.StateEnter += OnStateEnter; 
    }

    private void OnStateEnter(GameState state)
    {
        switch (state.stateSO.stateName)
        {
            case "Normal Mode":
                SetEnvironment( normalMusic);
                break;
            case "Nightmare Mode":
                SetEnvironment( nightmareMusic);
                break;
        }
    }

    private void SetEnvironment(AudioClip music)
    {

        if (musicSource != null && music != null)
        {
            musicSource.clip = music;
            musicSource.Play();
        }
    }

    private void OnDestroy()
    {
        gameStateChannel.StateEnter -= OnStateEnter;
    }
}


