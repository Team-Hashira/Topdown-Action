using UnityEngine;
using DG.Tweening;

public class MineableItem : MonoBehaviour, IDamageable
{
    private SpriteRenderer _spriteRenderer;
    private Material _material;
    private readonly int _blinkValue = Shader.PropertyToID("_Blink");
    [SerializeField]
    private ParticleSystem _particle;
    [SerializeField]
    private int _health = 3;
    [SerializeField]
    private ItemSO _item;

    private Tween _blinkTween;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _material = _spriteRenderer.material;
    }

    public void ApplyDamage(GameObject owner)
    {
        if (_blinkTween != null && _blinkTween.IsActive())
            _blinkTween.Kill();
        _health--;
        if (_health <= 0)
        {
            Item item = new Item();
            item.ItemInit(_item, 1);
            InventoryManager.Instance.AddItem(EInventory.Main, item);
        }
        CameraManager.Instance.ShakeCamera(2, 1, 0.15f);
        _material.SetFloat(_blinkValue, 1);
        _blinkTween = DOTween.To(() => _material.GetFloat(_blinkValue), v => _material.SetFloat(_blinkValue, v), 0, 0.1f);
        ParticleSystem p = Instantiate(_particle, transform.position, Quaternion.identity);
        p.Play();
    }
}
