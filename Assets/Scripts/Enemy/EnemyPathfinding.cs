using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EnemyPathfinding : IFixedTickable, IDisposable
{
    private readonly Transform _enemy;

    private readonly float _moveSpeed;
    private readonly Rigidbody2D _rb;
    private Vector2 _moveDirection;

    [Inject]
    public EnemyPathfinding(EnemyComponentProvider componentProvider)
    {
        _enemy = componentProvider.transform;
        _rb = componentProvider.Rigidbody2D;

        _moveSpeed = componentProvider.MoveSpeed;
        _moveDirection = Vector2.zero;
    }

    public void Dispose()
    {
        _moveDirection = Vector2.zero;
    }

    public void FixedTick()
    {
        _rb.MovePosition(_rb.position + _moveDirection * (_moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition)
    {
        _moveDirection = (targetPosition - (Vector2)_enemy.position).normalized;
    }
}