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

        annora.AudioClips.PlayMuerteCerteSound();
        annora.attackDetails.damageAmount = annoraData.muerteCerteDmg;
        CanUse = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(isAnimationFinished)
        {
            isDone = true;
            annora.attackDetails.damageAmount = annoraData.basicAtkDmg;
            annora.A4Hitbox.SetActive(false);
        }
        else if (annora.Anim.GetFloat("A4HitboxActive") == 1)
        {
            annora.A4Hitbox.SetActive(true);
        }
        else if (annora.Anim.GetFloat("A4HitboxActive") == 0)
        {
            annora.A4Hitbox.SetActive(false);
        }
    }

    public void ResetA4() => CanUse = true;
}
