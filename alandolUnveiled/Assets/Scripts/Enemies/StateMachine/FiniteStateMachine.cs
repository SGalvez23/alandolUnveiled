using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    public State currentState {get ; private set;}

    public void Initialize(State startingState){

        currentState = startingState;
        currentState.EnterState();
    }

    public void ChangeState(State newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

}
