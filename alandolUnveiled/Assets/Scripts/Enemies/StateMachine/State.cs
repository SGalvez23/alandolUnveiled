using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State 
{
   protected FiniteStateMachine stateMachine;
   protected Entity entity;

   protected float startTime;
   protected string animBoolName;

   public State(Entity etity, FiniteStateMachine stateMachine, string animBoolName)
   {
        this.entity = etity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
   }

   public virtual void EnterState()
   {
        startTime = Time.time;
        entity.anim.SetBool(animBoolName, true);
   }
   public virtual void ExitState()
   {
     entity.anim.SetBool(animBoolName, false);
   }
   public virtual void UpdateState()
   {
    
   }
   public virtual void PhysicsUpdate()
   {
    
   }
}
