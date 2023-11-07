using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public AnnoraAbility ability;
    Annora annora;
    float cooldownTime;
    float activeTime;

    enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }

    AbilityState state = AbilityState.Ready;

    private void Start()
    {
        Debug.Log("hola");
        annora = GetComponentInParent<Annora>();
    }

    void Update()
    {
        switch (state)
        {
            case AbilityState.Ready:
                if (annora.InputHandler.Ability1Input)
                {
                    ability.Activate(annora);
                    activeTime = ability.activeTime;
                    state = AbilityState.Active;
                }
                break;
            case AbilityState.Active:
                if(activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    ability.Deactivate(annora);
                    cooldownTime = ability.cooldownTime;
                    state = AbilityState.Cooldown;
                }
                break;
            case AbilityState.Cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    Debug.Log("ready");
                    state = AbilityState.Ready;
                    //ability.ResetAbility(annora);
                }
                break;
        }
    }
}
