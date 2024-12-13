using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Attack", story: "[agent] attack", category: "Action", id: "fbb8eda785fa8b8cbde3597d9b5d282d")]
public partial class AttackAction : Action
{
    [SerializeReference] public BlackboardVariable<Enemy> Agent;
    protected override Status OnStart()
    {
        Vector2 dir = Agent.Value.CurrentTarget.transform.position - Agent.Value.transform.position;
        dir = dir.normalized;
        Agent.Value.EnemyAttack.Attack(dir);
        return Status.Success;
    }
}

