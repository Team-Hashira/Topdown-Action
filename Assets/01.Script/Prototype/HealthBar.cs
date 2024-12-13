using System;
using UnityEngine;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    private Transform _pivotTrm;
    private Material _barMaterial;
    private Material _borderMaterial;

    private readonly int _blinkValue = Shader.PropertyToID("_Blink");

    private Sequence _onHitSequence;

    [SerializeField]
    private HealthPrototype _health;

    private void Awake()
    {
        _pivotTrm = transform.Find("Border/Pivot");
        _barMaterial = transform.Find("Border").GetComponent<SpriteRenderer>().material;
        _borderMaterial = _pivotTrm.Find("Bar").GetComponent<SpriteRenderer>().material;

        _health.OnHPChangeEvent += HandleOnHPChangeEvent;
    }

    private void HandleOnHPChangeEvent(float prev, float current)
    {
        if (_onHitSequence != null && _onHitSequence.IsActive())
            _onHitSequence.Complete();
        float ratio = current / _health.MaxHP;
        _pivotTrm.transform.localScale = new Vector2(ratio, 1);
        _onHitSequence = DOTween.Sequence();
        _barMaterial.SetFloat(_blinkValue, 1);
        _borderMaterial.SetFloat(_blinkValue, 1);
        _onHitSequence.Append(DOTween.To(() => _barMaterial.GetFloat(_blinkValue), v => _barMaterial.SetFloat(_blinkValue, v), 0, 0.15f));
        _onHitSequence.Join(DOTween.To(() => _borderMaterial.GetFloat(_blinkValue), v => _borderMaterial.SetFloat(_blinkValue, v), 0, 0.15f));
        _onHitSequence.Join(transform.DOLocalMoveY(transform.localPosition.y - 0.15f, 0.075f).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutSine));
    }
}
