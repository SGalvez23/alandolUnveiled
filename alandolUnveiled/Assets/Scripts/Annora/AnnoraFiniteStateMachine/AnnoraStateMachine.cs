using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnoraStateMachine
{
    public AnnoraState CurrentState { get; private set; }

    public void Initialize(AnnoraState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(AnnoraState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
