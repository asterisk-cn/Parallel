using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EnemyPathfinding : IFixedTickable, IDisposable
{
    private readonly KnockBack _knockBack;
    private readonly float _moveSpeed;
    private readonly Rigidbody2D _rb;

    private Vector2 _moveDirection;

    [Inject]
    public EnemyPathfinding(
        Rigidbody2D rb,
        KnockBack knockBack,
        EnemyParameter enemyParameter
    )
    {
        _rb = rb;
        _knockBack = knockBack;

        _moveSpeed = enemyParameter.MoveSpeed;
        _moveDirection = Vector2.zero;
    }

    public void Dispose()
    {
        _moveDirection = Vector2.zero;
    }

    public void FixedTick()
    {
        if (_knockBack.IsGettingKnockBack) return;
        _rb.MovePosition(_rb.position + _moveDirection * (_moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition)
    {
        _moveDirection = (targetPosition - _rb.position).normalized;
    }
}