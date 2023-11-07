using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnnoraAbility : ScriptableObject
{
    public float cooldownTime;
    public float activeTime;

    public virtual void Activate(Annora annora)
    {
        
    }

    public virtual void Deactivate(Annora annora)
    {
        
    }

    public virtual void ResetAbility(Annora annora)
    {
        
    }
}
