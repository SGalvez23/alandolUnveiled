using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Annora_A1State : AnnoraAbilityState
{
    public bool CanUse { get; private set; }
    public Annora_A1State(Annora annora, AnnoraStateMachine stateMachine, AnnoraData annoraData, string animBoolName) : base(annora, stateMachine, annoraData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        annora.AudioClips.PlayCamoSound();

        CanUse = false;
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

    public void ResetA1() => CanUse = true;
}
