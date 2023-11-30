using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Camo_Ability : AnnoraAbility
{
    public Material CamoMat;
    public Material DefMat;
    public float trueMoveVel;
    public float increasedVel;
    SpriteRenderer spriteRenderer;

    public override void Activate(Annora annora)
    {
        activeTime = annora.annoraData.camoTime;
        spriteRenderer = annora.SpriteRend;
        trueMoveVel = annora.annoraData.movementVel;
        increasedVel = trueMoveVel + (trueMoveVel * 0.3f);

        annora.annoraData.movementVel = increasedVel;
        annora.CamoMask();
        spriteRenderer.material = CamoMat;
    }

    public override void Deactivate(Annora annora)
    {
        cooldownTime = annora.annoraData.camoCoolTime;
        spriteRenderer = annora.SpriteRend;


        annora.annoraData.movementVel = trueMoveVel;
        annora.RmCamoMask();
        spriteRenderer.material = DefMat;
    }

    public override void ResetAbility(Annora annora)
    {
        annora.CamoState.ResetA1();
    }
}
