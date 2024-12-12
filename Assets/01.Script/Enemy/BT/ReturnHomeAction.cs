using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ReturnHome", story: "[agent] return at his home", category: "Action", id: "83d78da455341f0f4c4f7698939eb545")]
public partial class ReturnHomeAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Agent;

    protected override Status OnUpdate()
    {
        Agent.Value.SetDestination(Agent.Value.SelfAreaPosition);
        if(Agent.Value.isStopped == false)
            return Status.Running;
        else 
            return Status.Success;
    }
}

