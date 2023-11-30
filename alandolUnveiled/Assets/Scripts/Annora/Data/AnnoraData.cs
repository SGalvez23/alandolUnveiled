using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAnnoraData", menuName = "Data/Annora Data/Base Data")]
public class AnnoraData : ScriptableObject
{
    [Header("Atributos Generals")]
    public float health = 100;
    public int vidas = 2;
    public float basicAtkDmg = 15f;

    public float damageHopSpeed = 3f;

    [Header("Move State")]
    public float movementVel = 10f;

    [Header("Jump State")]
    public float jumpVel = 15f;
    public int totalJumps = 2;

    [Header("InAir State")]
    public float coyoteTime = 0.2f;
    public float jumpHeightMultiplier = 0.5f;

    [Header("Check Variables")]
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;

    [Header("Habilidades Annora")]
    public float launchForce = 0;
    public float camoTime = 3f;
    public float camoCoolTime = 6f;
    public float frenesiTime = 5f;
    public float frenesiCoolTime = 12f;
    public float apretonTime = 10f;
    public float apretonCoolTime = 3f;
    public float muerteCerteTime = 2f;
    public float muerteCoolTime = 15f;
    public float muerteCerteDmg = 40f;
}
