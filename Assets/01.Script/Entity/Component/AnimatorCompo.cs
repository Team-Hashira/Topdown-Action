using System;
using UnityEngine;

[Flags]
public enum EAnimTrigger
{
    Start = 1,
    Effect = 2,
    AttackReady = 4,
    Attack = 8,
    End = 16,
}

[RequireComponent(typeof(Animator))]
public class AnimatorCompo : MonoBehaviour, IEntityInitComponent
{
    private Animator _animator;

    public Action<EAnimTrigger> OnAnimationTriggerEvent;

    public void Initialize(Entity unit)
    {
        _animator = GetComponent<Animator>();
    }

    public void SetParam(AnimParamSO _paramSO, bool value)
        => _animator.SetBool(_paramSO.paramHash, value);
    public void SetParam(AnimParamSO _paramSO, int value)
        => _animator.SetInteger(_paramSO.paramHash, value);
    public void SetParam(AnimParamSO _paramSO, float value)
        => _animator.SetFloat(_paramSO.paramHash, value);
    public void SetParam(AnimParamSO _paramSO)
        => _animator.SetTrigger(_paramSO.paramHash);

    public void SetAnimationSpeed(float speed)
    {
        _animator.speed = speed;
    }

    private void AnimationTrigger(EAnimTrigger eAnimTrigger)
    {
        OnAnimationTriggerEvent?.Invoke(eAnimTrigger);
    }
}
