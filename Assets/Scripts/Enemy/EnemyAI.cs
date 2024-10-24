using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using VContainer.Unity;
using Random = UnityEngine.Random;

public class EnemyAI : IPostInitializable, IDisposable
{
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly EnemyPathfinding _pathfinding;
    private readonly State _state;

    public EnemyAI(EnemyPathfinding pathfinding)
    {
        _pathfinding = pathfinding;

        _cancellationTokenSource = new CancellationTokenSource();
        _state = State.Roaming;
    }

    public void Dispose()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
    }

    public void PostInitialize()
    {
        Observable
            .Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .Subscribe(_ => Roam())
            .AddTo(_cancellationTokenSource.Token);
    }

    private void Roam()
    {
        if (_state != State.Roaming) return;

        var roamPosition = GetRoamingPosition();
        _pathfinding.MoveTo(roamPosition);
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
    }

    private enum State
    {
        Roaming
    }
}