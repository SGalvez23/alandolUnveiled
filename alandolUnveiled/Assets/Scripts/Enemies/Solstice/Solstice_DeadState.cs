using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solstice_DeadState : DeadState
{
    private Solstice solstice;

    public Solstice_DeadState(Enemy enemy, FiniteStateMachine stateMachine, string animBoolName, Data_DeadState stateData, Solstice solstice) : base(enemy, stateMachine, animBoolName, stateData)
    {
        this.solstice = solstice;
    }
}
