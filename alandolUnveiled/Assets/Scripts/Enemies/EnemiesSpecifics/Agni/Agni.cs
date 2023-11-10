using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agni : Entity
{

   

    public Agni_IdleState idleState {get; private set;}
    public Agni_MoveState moveState  {get; private set;}

    [SerializeField]
    private D_Idle idleStateData;
    
    [SerializeField]
    private D_Move moveStateData;

    public override void Start ()
    {
        base.Start();
        
        moveState = new Agni_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new Agni_IdleState(this, stateMachine, "idle", idleStateData, this);
        stateMachine.Initialize(moveState);
    }
}
