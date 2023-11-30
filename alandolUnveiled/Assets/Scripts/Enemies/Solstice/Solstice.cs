using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solstice : Enemy
{
    public Solstice_IdleState IdleState { get; private set; }
    public Solstice_MoveState MoveState { get; private set; }
    public Solstice_PlayerDetectedState PlayerDetectedState { get; private set; }
    public Solstice_ChargeState ChargeState { get; private set; }
    public Solstice_LookForPlayerState LookForPlayerState { get; private set; }
    public Solstice_MeleeAttackState MeleeAttackState { get; private set; }

    [SerializeField]
    private Data_IdleState idleStateData;
    [SerializeField]
    private Data_MoveState moveStateData;
    [SerializeField]
    private Data_PlayerDetectedState playerDetectedData;
    [SerializeField]
    private Data_ChargeState chargeStateData;
    [SerializeField]
    private Data_LookForPlayerState lookForPlayerStateData;
    [SerializeField]
    private Data_MeleeAttackState meleeAttackStateData;

    [SerializeField]
    private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();

        MoveState = new Solstice_MoveState(this, stateMachine, "move", moveStateData, this);
        IdleState = new Solstice_IdleState(this, stateMachine, "idle", idleStateData, this);
        PlayerDetectedState = new Solstice_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedData, this);
        ChargeState = new Solstice_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        LookForPlayerState = new Solstice_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        MeleeAttackState = new Solstice_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);

        stateMachine.Initialize(MoveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
