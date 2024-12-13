using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FindTarget", story: "[Agent] is Find Target", category: "Action", id: "932ba4cb16ae963dfdb5f5a28526260e")]
public partial class FindTarget : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Agent;
   
    protected override Status OnStart()
    {
        var col = Physics2D.OverlapCircle(Agent.Value.SelfAreaPosition, Agent.Value.selfAreaRadius, LayerMask.GetMask("Player"));

        if (col)
        {
            Agent.Value.CurrentTarget = col.transform;
            return Status.Success;
        }
        else
        {
            Agent.Value.CurrentTarget = null;
            return Status.Failure;
        }
        
    }
}

