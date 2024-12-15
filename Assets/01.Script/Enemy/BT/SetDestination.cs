using System;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetDestination", story: "[Agent] Move", category: "Action", id: "cb03fec1717959ec54326f3931c4145b")]
public partial class SetDestinationAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Agent;
    protected override Status OnStart()
    {
        Agent.Value.Move();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        var col = Physics2D.OverlapCircle(Agent.Value.SelfAreaPosition, Agent.Value.selfAreaRadius, LayerMask.GetMask("Player"));
        if(col == null)
            return Status.Failure;

        var target = Physics2D.OverlapCircle(Agent.Value.transform.position, 10, LayerMask.GetMask("Player"));
        
        
        if(target == null)
            return Status.Failure;
        
        Agent.Value.SetDestination(target.transform.position);
        if(Agent.Value.isStopped == false)
            return Status.Running;
        else 
            return Status.Success;
    }
}

