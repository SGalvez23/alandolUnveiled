using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAnnoraData", menuName = "Data/Annora Data/Base Data")]
public class AnnoraData : ScriptableObject
{
    [Header("Atributos Generals")]
    public int health = 100;

    [Header("Move State")]
    public float movementVel = 10f;

    [Header("Jump State")]
    public float jumpVel = 15f;
    public int amountJumps = 1;

    [Header("InAir State")]
    public float coyoteTime = 0.2f;
    public float jumpHeightMultiplier = 0.5f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;

    [Header("Habilidades Annora")]
    public float launchForce = 0;
}
