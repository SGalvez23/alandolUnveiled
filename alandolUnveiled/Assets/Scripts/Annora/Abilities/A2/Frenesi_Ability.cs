using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Frenesi_Ability : AnnoraAbility
{
    public float trueMoveVel;
    public float increasedVel;
    public GameObject vfx;

    public override void Activate(Annora annora)
    {
        activeTime = annora.annoraData.frenesiTime;
        cooldownTime = annora.annoraData.frenesiCoolTime;
        trueMoveVel = annora.annoraData.movementVel;
        increasedVel = trueMoveVel + (trueMoveVel * 0.5f);
        ParticleSystem frenesiEffect = annora.GetComponentInChildren<ParticleSystem>();

        frenesiEffect.Play();
        annora.annoraData.movementVel = increasedVel;
        annora.attackDetails.damageAmount = annora.annoraData.frenesiDmg;
        annora.Anim.SetFloat("basicAtkSpeed", 1.35f);
    }

    public override void Deactivate(Annora annora)
    {
        ParticleSystem frenesiEffect = annora.GetComponentInChildren<ParticleSystem>();
        
        frenesiEffect.Stop();
        annora.annoraData.movementVel = trueMoveVel;
        annora.attackDetails.damageAmount = annora.annoraData.basicAtkDmg;
        annora.Anim.SetFloat("basicAtkSpeed", 1);
    }

    public override void ResetAbility(Annora annora)
    {
        annora.FrenesiState.ResetA2();
    }
}
