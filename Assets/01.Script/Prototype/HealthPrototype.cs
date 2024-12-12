using Crogen.CrogenPooling;
using System;
using UnityEngine;

public class HealthPrototype : MonoBehaviour, IDamageable
{
    public delegate void OnHPChangeDelegate(float prev, float current);
    [field: SerializeField] 
    public float HP { get; private set; }
    [field: SerializeField] 
    public float MaxHP { get; private set; }

    [SerializeField] 
    private EffectPoolType _hitEffect = EffectPoolType.HitEffect;
    public event OnHPChangeDelegate OnHPChangeEvent;
    public event Action OnDieEvent;

    private bool _isDead = false;

    public void ApplyDamage(GameObject owner)
    {
        gameObject.Pop(_hitEffect, transform.position, Quaternion.identity);
        float prev = HP;
        --HP;
        OnHPChangeEvent?.Invoke(prev, HP);
        if (HP <= 0.0f)
        {
            if (_isDead) return;
            _isDead = true;
            OnDie();
        }
    }

    private void OnDie()
    {
        OnDieEvent?.Invoke();
    }
}
