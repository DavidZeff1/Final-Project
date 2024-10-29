using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateListener : MonoBehaviour
{
    [SerializeField] private Light environmentLight;
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
                SetEnvironment(Color.white, 1f, normalMusic);
                break;
            case "Nightmare Mode":
                SetEnvironment(Color.red, 0.2f, nightmareMusic);
                break;
        }
    }

    private void SetEnvironment(Color lightColor, float intensity, AudioClip music)
    {
        if (environmentLight != null)
        {
            environmentLight.color = lightColor;
            environmentLight.intensity = intensity;
        }

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


