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

    public enum A1State
    {
        Ready,
        Active,
        Cooldown
    }

    public enum A2State
    {
        Ready,
        Active,
        Cooldown
    }

    public enum A3State
    {
        Ready,
        Active,
        Cooldown
    }

    public enum A4State
    {
        Ready,
        Active,
        Cooldown
    }

    public enum AbilityInput
    {
        A1,
        A2,
        A3,
        A4
    }

    public AbilityInput AInput;

    public A1State state1 = A1State.Ready;
    public A2State state2 = A2State.Ready;
    public A3State state3 = A3State.Ready;
    public A4State state4 = A4State.Ready;

    private void Start()
    {
        annora = GetComponentInParent<Annora>();
    }

    void Update()
    {
        ability1Input = annora.InputHandler.Ability1Input;
        ability2Input = annora.InputHandler.Ability2Input;
        ability3Input = annora.InputHandler.Ability3Input;
        ability4Input = annora.InputHandler.Ability4Input;

        switch (state1)
        {
            case A1State.Ready:
                if (ability1Input)
                {
                    ability[0].Activate(annora);
                    activeTime = ability[0].activeTime;
                    cooldownTime = ability[0].cooldownTime;
                    Debug.Log(activeTime);
                    state1 = A1State.Active;
                }
                break;
            case A1State.Active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    ability[0].Deactivate(annora);
                    Debug.Log(cooldownTime);
                    state1 = A1State.Cooldown;
                }
                break;
            case A1State.Cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    ability[0].ResetAbility(annora);
                    Debug.Log("ready");
                    state1 = A1State.Ready;
                }
                break;
        }

        switch (state2)
        {
            case A2State.Ready:
                if (ability2Input)
                {
                    ability[1].Activate(annora);
                    activeTime = ability[1].activeTime;
                    cooldownTime = ability[1].cooldownTime;
                    Debug.Log(activeTime);
                    state2 = A2State.Active;
                }
                break;
            case A2State.Active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    ability[1].Deactivate(annora);
                    Debug.Log(cooldownTime);
                    state2 = A2State.Cooldown;
                }
                break;
            case A2State.Cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    ability[1].ResetAbility(annora);
                    Debug.Log("ready");
                    state2 = A2State.Ready;
                }
                break;
        }

        switch (state3)
        {
            case A3State.Ready:
                if (ability3Input)
                {
                    ability[2].Activate(annora);
                    activeTime = ability[2].activeTime;
                    cooldownTime = ability[2].cooldownTime;
                    Debug.Log(activeTime);
                    state3 = A3State.Active;
                }
                break;
            case A3State.Active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    ability[1].Deactivate(annora);
                    Debug.Log(cooldownTime);
                    state3 = A3State.Cooldown;
                }
                break;
            case A3State.Cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    ability[1].ResetAbility(annora);
                    Debug.Log("ready");
                    state3 = A3State.Ready;
                }
                break;
        }

        switch (state4)
        {
            case A4State.Ready:
                if (ability4Input)
                {
                    ability[3].Activate(annora);
                    activeTime = ability[3].activeTime;
                    cooldownTime = ability[3].cooldownTime;
                    Debug.Log(activeTime);
                    state4 = A4State.Active;
                }
                break;
            case A4State.Active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    ability[3].Deactivate(annora);
                    Debug.Log(cooldownTime);
                    state4 = A4State.Cooldown;
                }
                break;
            case A4State.Cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    ability[3].ResetAbility(annora);
                    Debug.Log("ready");
                    state4 = A4State.Ready;
                }
                break;
        }
    }
}
