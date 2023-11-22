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
        annora.A4Effect.SetActive(true);
    }

    public override void Deactivate(Annora annora)
    {
        cooldownTime = annora.annoraData.muerteCoolTime;
        annora.A4Effect.SetActive(false);
    }

    public override void ResetAbility(Annora annora)
    {
        annora.MuerteCerteState.ResetA4();
    }
}
