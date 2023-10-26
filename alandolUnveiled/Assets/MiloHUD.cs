using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiloHUD : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] CanvasScaler scaler;
    public Image[] abilites;
    MainPlayer player;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        scaler = GetComponent<CanvasScaler>();
        player = GetComponentInParent<MainPlayer>();
    }

    private void Start()
    {
        canvas.worldCamera = Camera.main;
    }

    private void Update()
    {
        if (!player.ViejonState.CanUse)
        {
            abilites[0].enabled = false;
        }
        else if (player.ViejonState.CanUse)
        {
            abilites[0].enabled = true;
        }
    }
}
