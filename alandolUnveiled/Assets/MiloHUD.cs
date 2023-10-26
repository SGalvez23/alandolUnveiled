using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiloHUD : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] CanvasScaler scaler;
    public Image[] abilites;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        scaler = GetComponent<CanvasScaler>();
    }

    private void Start()
    {
        canvas.worldCamera = Camera.main;

    }
}
