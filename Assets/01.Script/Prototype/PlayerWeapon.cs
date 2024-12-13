using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private Animator _animator;

    private int _currentAttackCount = 0;
    [SerializeField]
    private float _weaponDelay;
    [SerializeField]
    private float _comboHoldTime = 0.8f;
    private float _lastAttackTime;
    [SerializeField]
    private bool _isAnimationEnd = true;
    [SerializeField]
    private float _attackRadius;
    [SerializeField]
    private LayerMask _whatIsEnemy;

    private Collider2D[] _targets;
    [SerializeField]
    private int _maxDetectEnemy;

    private PlayerController _player;

    public void Initialize(PlayerController player)
    {
        _player = player;
        _targets = new Collider2D[_maxDetectEnemy];
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(_lastAttackTime + _comboHoldTime < Time.time && _isAnimationEnd && _currentAttackCount != 0)
        {
            _currentAttackCount = 0;
            _animator.SetInteger("AttackCounter", _currentAttackCount);
            _animator.SetTrigger("Initialize");
        }
    }

    public void Attack()
    {
        if (_lastAttackTime + _weaponDelay > Time.time || !_isAnimationEnd) return;

        int count = Physics2D.OverlapCircle(_player.transform.position+ Vector3.right * _player.FacingDirection, _attackRadius, new ContactFilter2D() {layerMask = _whatIsEnemy, useLayerMask = true, useTriggers = true }, _targets);
        for(int i = 0; i < count; i++)
        {
            if (_targets[i].TryGetComponent(out IDamageable target))
            {
                target.ApplyDamage(_player.gameObject);
            }
        }
        _currentAttackCount %= 3;
        _currentAttackCount++;
        _animator.SetInteger("AttackCounter", _currentAttackCount);
        _isAnimationEnd = false;
    }

    private void AnimationEnd()
    {
        _lastAttackTime = Time.time;
        _isAnimationEnd = true;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (_player == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_player.transform.position + Vector3.right * _player.FacingDirection, _attackRadius);
    }
#endif
}
