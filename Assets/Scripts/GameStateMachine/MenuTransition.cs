using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTransition : TransitionBase
{
    [SerializeField] KeyCode key;
    bool menuPressed = false;
    bool canMenu = false;
    GameStateChannel gameStateChannel;

    protected override void Awake()
    {
        base.Awake();
        gameStateChannel = FindObjectOfType<Beacon>().gameStateChannel;
        gameStateChannel.StateEnter += StateEnter;
    }

    private void StateEnter(GameState state)
    {
        canMenu = state.stateSO.canMenu;
        menuPressed = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            menuPressed = true;
        }
    }

    public override bool ShouldTransition()
    {
        var canTransition = menuPressed && canMenu;
        if (canTransition)
        {
            menuPressed = false; 
        }

        return base.ShouldTransition() && canTransition;
    }

    private void OnDestroy()
    {
        gameStateChannel.StateEnter -= StateEnter;
    }
}


