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

    public override void Activate(Annora annora)
    {
        activeTime = annora.annoraData.camoTime;
        SpriteRenderer spriteRenderer = annora.GetComponent<SpriteRenderer>();
        trueMoveVel = annora.annoraData.movementVel;
        increasedVel = trueMoveVel + (trueMoveVel * 0.3f);

        annora.annoraData.movementVel = increasedVel;
        spriteRenderer.material = CamoMat;
        //Debug.Log(increasedVel);
    }

    public override void Deactivate(Annora annora)
    {
        cooldownTime = annora.annoraData.camoCoolTime;
        SpriteRenderer spriteRenderer = annora.GetComponent<SpriteRenderer>();


        annora.annoraData.movementVel = trueMoveVel;
        spriteRenderer.material = DefMat;
    }

    public override void ResetAbility(Annora annora)
    {
        annora.CamoState.ResetA1();
    }
}
