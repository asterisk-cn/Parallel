using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class Flash : IPostInitializable, IDisposable
{
    private readonly float _flashDuration;
    private readonly SpriteRenderer _spriteRenderer;
    private readonly Material _whiteFlashMaterial;
    private CancellationTokenSource _cts;
    private Material _defaultMaterial;

    [Inject]
    public Flash(SpriteRenderer spriteRenderer, Material whiteFlashMaterial, EnemyParameter enemyParameter)
    {
        _spriteRenderer = spriteRenderer;
        _whiteFlashMaterial = whiteFlashMaterial;
        _flashDuration = enemyParameter.FlashDuration;
    }

    public void Dispose()
    {
        _cts.Cancel();
        _cts.Dispose();
    }

    public void PostInitialize()
    {
        _defaultMaterial = _spriteRenderer.material;
        _cts = new CancellationTokenSource();
    }

    public void ExecuteFlash()
    {
        _spriteRenderer.material = _whiteFlashMaterial;

        Observable
            .Timer(TimeSpan.FromSeconds(_flashDuration))
            .Subscribe(_ => { _spriteRenderer.material = _defaultMaterial; })
            .AddTo(_cts.Token);
    }
}