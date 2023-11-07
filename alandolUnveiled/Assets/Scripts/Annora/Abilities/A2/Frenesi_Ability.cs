using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Frenesi_Ability : AnnoraAbility
{
    public float trueMoveVel;
    public float increasedVel;


    public override void Activate(Annora annora)
    {
        activeTime = annora.annoraData.frenesiTime;
        trueMoveVel = annora.annoraData.movementVel;
        increasedVel = trueMoveVel + (trueMoveVel * 0.5f);

        annora.annoraData.movementVel = increasedVel;
        annora.Anim.SetFloat("basicAtkSpeed", 1.15f);
    }

    public override void Deactivate(Annora annora)
    {
        cooldownTime = annora.annoraData.frenesiCoolTime;

        annora.annoraData.movementVel = trueMoveVel;
        annora.Anim.SetFloat("basicAtkSpeed", 1);
    }

    public override void ResetAbility(Annora annora)
    {
        
    }
}
