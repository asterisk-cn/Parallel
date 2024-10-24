using System;
using R3;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

public class EnemyHealth : IPostInitializable, IDisposable
{
    private readonly int _maxHealth;

    private ReactiveProperty<int> _currentHealth;
    private GameObject _enemy;

    public EnemyHealth(EnemyComponentProvider componentProvider)
    {
        _enemy = componentProvider.gameObject;
        _maxHealth = componentProvider.MaxHealth;
    }

    public void Dispose()
    {
        _enemy = null;

        _currentHealth.Dispose();
    }

    public void PostInitialize()
    {
        _currentHealth = new ReactiveProperty<int>(_maxHealth);

        _currentHealth
            .Where(health => health <= 0)
            .Subscribe(_ => OnDeath());
    }

    public void TakeDamage(int damage)
    {
        _currentHealth.Value = Mathf.Max(0, _currentHealth.Value - damage);
    }

    private void OnDeath()
    {
        Object.Destroy(_enemy);
        Dispose();
    }
}