using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Apreton_Ability : AnnoraAbility
{
    public override void Activate(Annora annora)
    {
        activeTime = annora.annoraData.apretonTime;
        annora.StartA3();
    }

    public override void Deactivate(Annora annora)
    {
        cooldownTime = annora.annoraData.apretonCoolTime;
        annora.StopA3();
    }

    public override void ResetAbility(Annora annora)
    {
        annora.ApretonState.ResetA3();
    }
}
