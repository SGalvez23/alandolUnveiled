using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnnoraHUD : MonoBehaviour
{
    Annora annora;
    AbilityHolder abilityHolder;

    [Header("Vida")]
    public Image vida;

    [Header("Camuflage")]
    public Image A1Image;
    public float A1CoolTime;
    [SerializeField] bool isCooldownA1;

    [Header("Frenesi de Sangre")]
    public Image A2Image;
    public float A2CoolTime;
    [SerializeField] bool isCooldownA2;

    [Header("Apreton de Manos")]
    public Image A3Image;
    public float A3CoolTime;
    [SerializeField] bool isCooldownA3;

    [Header("Muerte Certera")]
    public Image A4Image;
    public float A4CoolTime;
    [SerializeField] bool isCooldownA4;

    private void Start()
    {
        if (annora.photonView.IsMine)
        {
            annora = GetComponentInParent<Annora>();
            abilityHolder = GetComponentInParent<AbilityHolder>();
            A1CoolTime = annora.annoraData.camoCoolTime;
            A2CoolTime = annora.annoraData.frenesiCoolTime;
            A3CoolTime = annora.annoraData.apretonCoolTime;
            A4CoolTime = annora.annoraData.muerteCoolTime;

            isCooldownA1 = false;
            isCooldownA2 = false;
            isCooldownA3 = false;
            isCooldownA4 = false;

            A1Image.fillAmount = 0;
            A2Image.fillAmount = 0;
            A3Image.fillAmount = 0;
            A4Image.fillAmount = 0;
        }
    }

    private void Update()
    {
        if (annora.photonView.IsMine)
        {
            vida.fillAmount = annora.actualHealth / 100f;

            A1();
            A2();
            A3();
            A4();
        }
    }

    void A1()
    {
        if (abilityHolder.state1 == AbilityHolder.A1State.Active)
        {
            A1Image.fillAmount = 1;
        }

        if (!isCooldownA1 && abilityHolder.state1 == AbilityHolder.A1State.Cooldown)
        {
            isCooldownA1 = true;
        }

        if (isCooldownA1)
        {
            A1Image.fillAmount -= 1 / A1CoolTime * Time.deltaTime;

            if (A1Image.fillAmount <= 0)
            {
                A1Image.fillAmount = 0;
                isCooldownA1 = false;
            }
        }
    }
    void A2()
    {
        if(abilityHolder.state2 == AbilityHolder.A2State.Active)
        {
            A2Image.fillAmount = 1;
        }

        if (!isCooldownA2 && abilityHolder.state2 == AbilityHolder.A2State.Cooldown)
        {
            isCooldownA2 = true;
        }

        if (isCooldownA2)
        {
            A2Image.fillAmount -= 1 / A2CoolTime * Time.deltaTime;

            if (A2Image.fillAmount <= 0)
            {
                A2Image.fillAmount = 0;
                isCooldownA2 = false;
            }
        }
    }

    void A3()
    {
        if(abilityHolder.state3 == AbilityHolder.A3State.Active)
        {
            A3Image.fillAmount = 1;
        }

        if (!isCooldownA3 && abilityHolder.state3 == AbilityHolder.A3State.Cooldown)
        {
            isCooldownA3 = true;
        }

        if (isCooldownA3)
        {
            A3Image.fillAmount -= 1 / A3CoolTime * Time.deltaTime;

            if (A3Image.fillAmount <= 0)
            {
                A3Image.fillAmount = 0;
                isCooldownA3 = false;
            }
        }
    }


    void A4()
    {
        if(abilityHolder.state4 == AbilityHolder.A4State.Active)
        {
            A4Image.fillAmount = 1;
        }

        if (!isCooldownA4 && abilityHolder.state4 == AbilityHolder.A4State.Cooldown)
        {
            isCooldownA4 = true;
        }

        if (isCooldownA4)
        {
            A4Image.fillAmount -= 1 / A4CoolTime * Time.deltaTime;

            if (A4Image.fillAmount <= 0)
            {
                A4Image.fillAmount = 0;
                isCooldownA4 = false;
            }
        }
    }
}
