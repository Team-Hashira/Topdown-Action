using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private InputReaderSO _inputReader;
    private Rigidbody2D _rigidbody;
    private Transform _visualTrm;

    [SerializeField]
    private PlayerWeapon _weapon;

    [SerializeField]
    private float _moveSpeed = 3f;

    public float FacingDirection { get; private set; } = 1;

    private void Awake()
    {
        _inputReader.OnMovementEvent += HandleOnMovementEvent;
        _inputReader.OnAttackEvent += HandleOnAttackEvent;
        _inputReader.OnMouseMoveEvent += HandleOnMosueMoveEvent;
        _rigidbody = GetComponent<Rigidbody2D>();
        _visualTrm = transform.Find("Visual");
        _weapon.Initialize(this);
    }

    private void HandleOnMosueMoveEvent(Vector2 mousePos)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = worldPos - (Vector2)transform.position;
        direction.Normalize();
        float cross = Vector3.Cross(Vector3.up, direction).z; // 0보다 작으면 오른쪽
        if (cross < 0)
        {
            _visualTrm.transform.rotation = Quaternion.Euler(0, 0, 0);
            FacingDirection = 1;
        }
        else
        {
            _visualTrm.transform.rotation = Quaternion.Euler(0, 180, 0);
            FacingDirection = -1;
        }

    }

    private void HandleOnAttackEvent()
    {
        _weapon.Attack();
    }

    private void HandleOnMovementEvent(Vector2 movement)
    {
        _rigidbody.linearVelocity = movement * _moveSpeed;
    }
}
