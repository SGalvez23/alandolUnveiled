using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Annora_A4State : AnnoraAbilityState
{
    public bool CanUse { get; private set; }
    public Annora_A4State(Annora annora, AnnoraStateMachine stateMachine, AnnoraData annoraData, string animBoolName) : base(annora, stateMachine, annoraData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CanUse = false;
        //annora. hace la habilidad
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

    public void ResetA4() => CanUse = true;
}
