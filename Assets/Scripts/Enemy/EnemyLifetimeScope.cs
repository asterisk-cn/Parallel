using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EnemyLifetimeScope : LifetimeScope
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private EnemyHealthMonoBehaviour _enemyHealthMonoBehaviour;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Material _whiteFlashMaterial;
    [SerializeField] private EnemyParameter _parameter;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(gameObject);
        builder.RegisterInstance(_rigidbody2D);
        builder.RegisterInstance(_enemyHealthMonoBehaviour);
        builder.RegisterInstance(_parameter);
        builder.RegisterInstance(_spriteRenderer);
        builder.RegisterInstance(_whiteFlashMaterial);

        builder.Register<EnemyAI>(Lifetime.Scoped);
        builder.Register<EnemyPathfinding>(Lifetime.Scoped);
        builder.Register<EnemyHealth>(Lifetime.Scoped);
        builder.Register<KnockBack>(Lifetime.Scoped);
        builder.Register<Flash>(Lifetime.Scoped);

        builder.RegisterEntryPoint<EnemyEntryPoint>();
    }
}

// TODO: ScriptableObject
[Serializable]
public class EnemyParameter
{
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _knockBackDuration = 0.5f;
    [SerializeField] private float _flashDuration = 0.1f;

    public int MaxHealth => _maxHealth;
    public float MoveSpeed => _moveSpeed;
    public float KnockBackDuration => _knockBackDuration;
    public float FlashDuration => _flashDuration;
}