using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
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

    [Header("Habilidades Milo")]
    public float viejonTime = 4f;
    public float rojoVivoTime = 10f;
    public float cheveTime = 5f;
    public float carnitaTime = 5f;
}
