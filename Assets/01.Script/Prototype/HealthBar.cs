using System;
using UnityEngine;
using DG.Tweening;

public class HealthBar : MonoBehaviour
{
    private Transform _pivotTrm;
    private Material _barMaterial;
    private Material _borderMaterial;

    private readonly int _blinkValue = Shader.PropertyToID("_Blink");

    private Sequence _blinkSequence;

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
        if (_blinkSequence != null && _blinkSequence.IsActive())
            _blinkSequence.Kill();
        float ratio = current / _health.MaxHP;
        _pivotTrm.transform.localScale = new Vector2(1, ratio);
        _barMaterial.SetFloat(_blinkValue, 1);
        _borderMaterial.SetFloat(_blinkValue, 1);
        _blinkSequence.Append(DOTween.To(() => _barMaterial.GetFloat(_blinkValue), v => _barMaterial.SetFloat(_blinkValue, v), 0, 0.15f));
        _blinkSequence.Join(DOTween.To(() => _borderMaterial.GetFloat(_blinkValue), v => _borderMaterial.SetFloat(_blinkValue, v), 0, 0.15f));
    }
}
