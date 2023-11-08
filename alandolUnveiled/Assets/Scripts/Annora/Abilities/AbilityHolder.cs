using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public AnnoraAbility[] ability;
    Annora annora;
    float cooldownTime;
    float activeTime;
    bool ability1Input;
    bool ability2Input;
    bool ability3Input;
    bool ability4Input;
    int abs;

    enum AbilityState
    {
        Ready,
        Active,
        Cooldown
    }

    AbilityState state = AbilityState.Ready;

    private void Start()
    {
        annora = GetComponentInParent<Annora>();
        abs = 4;
    }

    void Update()
    {
        ability1Input = annora.InputHandler.Ability1Input;
        ability2Input = annora.InputHandler.Ability2Input;
        ability3Input = annora.InputHandler.Ability3Input;
        ability4Input = annora.InputHandler.Ability4Input;

        if (ability1Input)
        {
            abs = 0;
            Debug.Log("A1");
        }
        else if (ability2Input)
        {
            abs = 1;
            Debug.Log("A2");
        }
        else if (ability3Input)
        {
            abs = 2;
        }
        else if (ability4Input)
        {
            abs = 3;
        }

        switch (state)
        {
            case AbilityState.Ready:
                if (abs >= 0 && abs <= 3)
                {
                    ability[abs].Activate(annora);
                    activeTime = ability[abs].activeTime;
                    state = AbilityState.Active;
                }
                break;
            case AbilityState.Active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    ability[abs].Deactivate(annora);
                    cooldownTime = ability[abs].cooldownTime;
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
