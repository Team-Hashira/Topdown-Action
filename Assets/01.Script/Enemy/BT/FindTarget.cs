using System;
using Unity.Behavior;
using UnityEngine;
using Composite = Unity.Behavior.Composite;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FindTarget", story: "[I] Find target by [range]", category: "Flow", id: "7a04bc7c8d60ac840b66a33da1c95eda")]
public partial class FindTargetSequence : Composite
{
    [SerializeReference] public BlackboardVariable<Enemy> I;
    [SerializeReference] public BlackboardVariable<float> Range;
    protected override Status OnStart()
    {
        Debug.Log("find target...");
        var target = Physics2D.OverlapCircle(I.Value.transform.position, Range.Value, LayerMask.GetMask("Player"));

        if (target == null)
        {
            Debug.Log("not Fint");
            return Status.Failure;
        }
        I.Value.CurrentTarget = target.transform;
        Debug.Log("Fint!");
        return Status.Failure;
    }
}

