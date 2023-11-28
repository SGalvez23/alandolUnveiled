using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnoraDeadState : AnnoraAbilityState
{
    public AnnoraDeadState(Annora annora, AnnoraStateMachine stateMachine, AnnoraData annoraData, string animBoolName) : base(annora, stateMachine, annoraData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        annora.Death();
        isDone = true;
    }

    public override void Exit()
    {
        base.Exit();


    }

    public override void Update()
    {
        base.Update();
    }
}
