using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TransitionBase : MonoBehaviour
{
    protected GameState sourceState;
    [SerializeField] protected GameState targetState;
    public GameState TargetState { get { return targetState; }}

    protected virtual void Start()
    {
        sourceState = GetComponentInParent<GameState>();
        if(sourceState == null )
        {
            Debug.LogError($"Unable to find source state in {name}");
        }
        if(targetState == null )
        {
            Debug.LogError($"Unable to find target state in {name}");
        }
    }

    public virtual bool ShouldTransition()
    {
        return sourceState.isCurrentState && !sourceState.wasTransitionInto;
    }
}
