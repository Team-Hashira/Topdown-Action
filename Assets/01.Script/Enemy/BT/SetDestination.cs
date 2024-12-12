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
        Debug.Log("밍 시발");
        Agent.Value.Move();
        return Status.Success;
    }

    protected override void OnEnd()
    {
        Debug.Log("이동 끝남ㅋㅋ");
    }
}

