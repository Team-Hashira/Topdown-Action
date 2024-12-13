using UnityEngine;
using DG.Tweening;
using System;

public class MineableItem : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Material _material;
    private readonly int _blinkValue = Shader.PropertyToID("_Blink");
    [SerializeField]
    private ParticleSystem _particle;
    [SerializeField]
    private int _hp = 3;
    [SerializeField]
    private ItemSO _item;

    private Tween _blinkTween;
    [SerializeField]
    private HealthPrototype _health;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _material = _spriteRenderer.material;
        _health.OnHPChangeEvent += HandleOnHPChangeEvent;
        _health.OnDieEvent += HandleOnDieEvent;
    }

    private void HandleOnDieEvent()
    {
        Item item = new Item();
        item.ItemInit(_item, 1);
        Destroy(gameObject);
        //InventoryManager.Instance.AddItem(EInventory.Main, item);
    }

    private void HandleOnHPChangeEvent(float prev, float current)
    {
        if (_blinkTween != null && _blinkTween.IsActive())
            _blinkTween.Kill();
        CameraManager.Instance.ShakeCamera(1.5f, 1, 0.15f);
        _material.SetFloat(_blinkValue, 1);
        _blinkTween = DOTween.To(() => _material.GetFloat(_blinkValue), v => _material.SetFloat(_blinkValue, v), 0, 0.15f);
    }
}
