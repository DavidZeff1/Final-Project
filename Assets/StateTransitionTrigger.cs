using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTransitionTrigger : MonoBehaviour
{
    public GameState targetState;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var beacon = FindObjectOfType<Beacon>();
            beacon.gameStateChannel.StateEntered(targetState);
        }
    }
}

