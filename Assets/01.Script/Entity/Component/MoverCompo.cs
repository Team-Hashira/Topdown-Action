using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoverCompo : MonoBehaviour, IEntityInitComponent
{
    private Rigidbody2D _rigid2D;

    private Vector2 _movement;

    public void Initialize(Entity unit)
    {
        _rigid2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        _rigid2D.linearVelocity = _movement;
    }

    public void SetMove(Vector2 movement)
    {
        _movement = movement;
    }

    public void StopImmediat()
    {
        _movement = Vector2.zero;
    }
}
