using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MuerteCerte_Ability : AnnoraAbility
{
    public override void Activate(Annora annora)
    {
        activeTime = annora.annoraData.muerteCerteTime;
    }

    public override void Deactivate(Annora annora)
    {
        cooldownTime = annora.annoraData.muerteCoolTime;
    }

    public override void ResetAbility(Annora annora)
    {
        
    }
}
