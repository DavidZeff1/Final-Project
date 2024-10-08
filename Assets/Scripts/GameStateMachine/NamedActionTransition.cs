using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedActionTransition : TransitionBase
{
    [SerializeField] private string actionName;
    public string ActionName
    {
        get { return actionName; }
    }

    private bool actionDone = false;

    public void DoAction()
    {
        actionDone = true;
    }

    public override bool ShouldTransition()
    {
        if (!base.ShouldTransition())
            return false;

        if (actionDone)
        {
            actionDone = false;
            return true;
        }

        return actionDone;
    }
}
