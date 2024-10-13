using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    void Start()
    {
        // Start the coroutine to trigger the events after 10 seconds
        StartCoroutine(TriggerEventsAfterDelay(5f));
    }

    private IEnumerator TriggerEventsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Trigger both events
        GameEventManager.instance.TriggerBulletSpawnRateIncrease();
        GameEventManager.instance.TriggerPlayerMovementInversion();
    }
}

