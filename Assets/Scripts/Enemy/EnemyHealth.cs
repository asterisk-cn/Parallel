using System;
using R3;
using UnityEngine;
using VContainer.Unity;
using Object = UnityEngine.Object;

public class EnemyHealth : IPostInitializable, IDisposable
{
    private readonly Flash _flash;
    private readonly KnockBack _knockBack;
    private readonly int _maxHealth;

    private ReactiveProperty<int> _currentHealth;
    private GameObject _enemy;

    public EnemyHealth(
        GameObject enemy,
        KnockBack knockBack,
        EnemyParameter enemyParameter,
        Flash flash
    )
    {
        _enemy = enemy;
        _knockBack = knockBack;
        _maxHealth = enemyParameter.MaxHealth;
        _flash = flash;
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

    public void TakeDamage(int damage, Transform damageSource)
    {
        _currentHealth.Value = Mathf.Max(0, _currentHealth.Value - damage);
        _knockBack.GetKnockBack(damageSource, 15f);
        _flash.ExecuteFlash();
    }

    private void OnDeath()
    {
        Object.Destroy(_enemy);
        Dispose();
    }
}