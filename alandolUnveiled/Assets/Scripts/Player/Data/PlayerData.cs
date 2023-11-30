using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/MainPlayer Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Atributos Generals")]
    public float health = 100;
    public int vidas = 2;

    public float damageHopSpeed = 3f;
    public float touchDamage = 10f;

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
    public float launchForce = 2f;
    public float viejonTime = 8f;
    public float viejonCoolTime = 18f;
    //public float rojoVivoTime = 10f;
    public int rojoVivoCant = 3;
    public float rojoVivoCoolTime = 4f;
    public int cheveCant = 6;
    public float cheveCoolTime = 12;
    public int carnitaCant = 3;
    public float carnitaCoolTime = 16;
}
