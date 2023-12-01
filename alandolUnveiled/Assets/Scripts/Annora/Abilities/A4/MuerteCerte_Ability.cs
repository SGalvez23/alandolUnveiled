using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MuerteCerte_Ability : AnnoraAbility
{
    public GameObject vfx;
    public override void Activate(Annora annora)
    {
        activeTime = annora.annoraData.muerteCerteTime;
        annora.attackDetails.damageAmount = annora.annoraData.muerteCerteDmg;
        annora.A4Effect.SetActive(true);
        annora.CheckAttackHitBox();
    }

    public override void Deactivate(Annora annora)
    {
        cooldownTime = annora.annoraData.muerteCoolTime;
        annora.attackDetails.damageAmount = annora.annoraData.basicAtkDmg;
        annora.A4Effect.SetActive(false);
        annora.AnimationFinishTrigger();
    }

    public override void ResetAbility(Annora annora)
    {
        annora.MuerteCerteState.ResetA4();
    }
}
