using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EnemyLifetimeScope : LifetimeScope
{
    [SerializeField] private EnemyComponentProvider enemyComponentProvider;
    [SerializeField] private EnemyHealthMonoBehaviour enemyHealthMonoBehaviour;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(enemyComponentProvider);
        builder.RegisterComponent(enemyHealthMonoBehaviour);

        builder.Register<EnemyAI>(Lifetime.Scoped);
        builder.Register<EnemyPathfinding>(Lifetime.Scoped);
        builder.Register<EnemyHealth>(Lifetime.Scoped);

        builder.RegisterEntryPoint<EnemyEntryPoint>();
    }
}