using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class Data_Enemy : ScriptableObject
{
    public float maxHealth = 100f;

    public float damageHopSpeed = 5f;
    public float touchDamage = 5f;

    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;

    public float maxAggroDistance = 4f;
    public float minAggroDistance = 2f;

    public float closeRangeActionDistance = 1f;

    public AudioClip damagedSound;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;
}
