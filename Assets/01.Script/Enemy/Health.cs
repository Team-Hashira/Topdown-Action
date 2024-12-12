using Crogen.CrogenPooling;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [field:SerializeField] public float HP { get; private set; }
    [field:SerializeField] public float MaxHP { get; private set; }
    [SerializeField] private EffectPoolType _hitEffect = EffectPoolType.HitEffect;
    
    private bool _isDead = false;
    
    public void ApplyDamage(GameObject owner)
    {
        gameObject.Pop(_hitEffect, transform.position, Quaternion.identity);
        --HP;
        if (HP <= 0.0f)
        {
            if (_isDead) return;
            _isDead = true;
            OnDie();
        }
    }

    private void OnDie()
    {
        print("야야 얘 죽었나봨ㅋㅋㅋ");
    }
}
