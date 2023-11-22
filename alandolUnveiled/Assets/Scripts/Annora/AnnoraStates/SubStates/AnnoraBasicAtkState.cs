using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnoraBasicAtkState : AnnoraAbilityState
{
    public AnnoraBasicAtkState(Annora annora, AnnoraStateMachine stateMachine, AnnoraData annoraData, string animBoolName) : base(annora, stateMachine, annoraData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (isAnimationFinished)
        {
            isDone = true;
        }
        else if (annora.Anim.GetFloat("basicAtkHitBoxActive") == 1)
        {
            annora.basicHitbox.SetActive(true);
        }
        else if(annora.Anim.GetFloat("basicAtkHitBoxActive") != 1)
        {
            annora.basicHitbox.SetActive(false);
        }

        annora.CheckFlip(xInput);
        annora.SetXVelocity(annoraData.movementVel * xInput);

        annora.Anim.SetFloat("xVelocity", Mathf.Abs(annora.CurrentVelocity.x));
        annora.Anim.SetFloat("yVelocity", annora.CurrentVelocity.y);
    }
}
