using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnnoraHUD : MonoBehaviour
{
    Annora annora;

    [Header("Camuflage")]
    public Image A1Image;
    public float A1CoolTime;
    [SerializeField] bool isCooldownA1;
    public bool A1Input { get; private set; }

    [Header("Frenesi de Sangre")]
    public Image A2Image;
    public float A2CoolTime;
    [SerializeField] bool isCooldownA2;
    public bool A2Input { get; private set; }

    [Header("Apreton de Manos")]
    public Image A3Image;
    public float A3CoolTime;
    [SerializeField] bool isCooldownA3;
    public bool A3Input { get; private set; }

    [Header("Muerte Certera")]
    public Image A4Image;
    public float A4CoolTime;
    [SerializeField] bool isCooldownA4;
    public bool A4Input { get; private set; }

    private void Start()
    {
        annora = GetComponentInParent<Annora>();
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

    private void Update()
    {
        A1Input = annora.InputHandler.Ability1Input;
        A2Input = annora.InputHandler.Ability2Input;
        A3Input = annora.InputHandler.Ability3Input;
        A4Input = annora.InputHandler.Ability4Input;

        A1();
        A2();
        A3();
        A4();
    }

    void A1()
    {
        if (!isCooldownA1 && A1Input)
        {
            isCooldownA1 = true;
            A1Image.fillAmount = 1;
        }

        if (isCooldownA1)
        {
            A1Image.fillAmount -= 1 / A1CoolTime * Time.deltaTime;

            if (A1Image.fillAmount <= 0)
            {
                A1Image.fillAmount = 0;
                isCooldownA1 = false;
                annora.CamoState.ResetA1();
            }
        }
    }
    void A2()
    {
        if (!isCooldownA2 && A2Input)
        {
            isCooldownA2 = true;
            A2Image.fillAmount = 1;
        }

        if (isCooldownA2)
        {
            A2Image.fillAmount -= 1 / A2CoolTime * Time.deltaTime;

            if (A2Image.fillAmount <= 0)
            {
                A2Image.fillAmount = 0;
                isCooldownA2 = false;
                annora.FrenesiState.ResetA2();
            }
        }
    }

    void A3()
    {
        if (!isCooldownA3 && A3Input)
        {
            isCooldownA3 = true;
            A3Image.fillAmount = 1;
        }

        if (isCooldownA3)
        {
            A3Image.fillAmount -= 1 / A3CoolTime * Time.deltaTime;

            if (A3Image.fillAmount <= 0)
            {
                A3Image.fillAmount = 0;
                isCooldownA3 = false;
                annora.ApretonState.ResetA3();
            }
        }
    }


    void A4()
    {
        if (!isCooldownA4 && A4Input)
        {
            isCooldownA4 = true;
            A4Image.fillAmount = 1;
        }

        if (isCooldownA4)
        {
            A4Image.fillAmount -= 1 / A4CoolTime * Time.deltaTime;

            if (A4Image.fillAmount <= 0)
            {
                A4Image.fillAmount = 0;
                isCooldownA4 = false;
                annora.MuerteCerteState.ResetA4();
            }
        }
    }
}
