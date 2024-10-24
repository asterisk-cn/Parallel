using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using VContainer.Unity;

public class KnockBack : IPostInitializable, IDisposable
{
    private readonly float _knockBackDuration;
    private readonly Rigidbody2D _rb;
    private CancellationTokenSource _cts;

    public KnockBack(Rigidbody2D rb, EnemyParameter enemyParameter)
    {
        _rb = rb;
        _knockBackDuration = enemyParameter.KnockBackDuration;
    }

    public bool IsGettingKnockBack { get; private set; }

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }

    public void PostInitialize()
    {
        _rb.linearVelocity = Vector2.zero;
        _cts = new CancellationTokenSource();
    }

    public void GetKnockBack(Transform damageSource, float knockBackForce)
    {
        var direction = ((Vector2)_rb.transform.position - (Vector2)damageSource.position).normalized;
        _rb.AddForce(direction * knockBackForce * _rb.mass, ForceMode2D.Impulse);

        IsGettingKnockBack = true;

        Observable
            .Timer(TimeSpan.FromSeconds(_knockBackDuration))
            .Subscribe(_ => StopKnockBack())
            .AddTo(_cts.Token);
    }

    private void StopKnockBack()
    {
        _rb.linearVelocity = Vector2.zero;

        IsGettingKnockBack = false;
    }
}