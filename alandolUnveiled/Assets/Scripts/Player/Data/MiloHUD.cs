using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class MiloHUD : MonoBehaviourPunCallbacks, IPunObservable

{
    MainPlayer milo;

    [Header("Vida")]
    public Image vida;

    [Header("El Viejon")]
    public Image A1Image;
    public float A1CoolTime;
    [SerializeField]bool isCooldownA1;
    public bool A1Input { get; private set; }

    [Header("Al Rojo Vivo")]
    public Image A2Image;
    public float A2CoolTime;
    [SerializeField] bool isCooldownA2;
    public bool A2Input { get; private set; }

    [Header("Cheve")]
    public Image A3Image;
    public float A3CoolTime;
    [SerializeField] bool isCooldownA3;
    public bool A3Input { get; private set; }

    [Header("Carnita Asada")]
    public Image A4Image;
    public float A4CoolTime;
    [SerializeField] bool isCooldownA4;
    public bool A4Input { get; private set; }

    private void Start()
    {
        milo = GetComponentInParent<MainPlayer>();
        A1CoolTime = milo.playerData.viejonCoolTime;
        A2CoolTime = milo.playerData.rojoVivoCoolTime;
        A3CoolTime = milo.playerData.cheveCoolTime;
        A4CoolTime = milo.playerData.carnitaCoolTime;

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
        if (photonView.IsMine)
        {
            vida.fillAmount = milo.actualhealth / 100f;

            A1Input = milo.InputHandler.Ability1Input;
            A2Input = milo.InputHandler.Ability2Input;
            A3Input = milo.InputHandler.Ability3Input;
            A4Input = milo.InputHandler.Ability4Input;
            A1();
            A2();
            A3();
            A4();
        }
    }

    void A1()
    {
        if(!isCooldownA1 && A1Input)
        {
            isCooldownA1 = true;
            A1Image.fillAmount = 1;
        }

        if (isCooldownA1)
        {
            A1Image.fillAmount -= 1 / A1CoolTime * Time.deltaTime;

            if(A1Image.fillAmount <= 0)
            {
                A1Image.fillAmount = 0;
                isCooldownA1 = false;
                milo.ViejonState.ResetA1();
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
                milo.RojoVivoState.ResetA2();
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
                milo.CheveState.ResetA3();
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
                milo.CarnitaAsadaState.ResetA4();
            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Writing data to send over the network
            //stream.SendNext(transform.position);
            stream.SendNext(vida);
        }
        else
        {
            // Reading data received from the network
            //transform.position = (Vector3)stream.ReceiveNext();
            vida = (Image)stream.ReceiveNext();

        }
    }
}

