using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<PlayerInputHandler>(Lifetime.Scoped)
            .AsImplementedInterfaces()
            .AsSelf();
        builder.Register<PlayerController>(Lifetime.Scoped);
        builder.Register<SwordController>(Lifetime.Scoped);

        builder.RegisterComponentInHierarchy<PlayerMonoBehaviour>();
        builder.RegisterComponentInHierarchy<PlayerComponentProvider>();
        builder.RegisterComponentInHierarchy<SwordMonoBehaviour>();
        builder.RegisterComponentInHierarchy<SwordComponentProvider>();
        builder.RegisterComponentInHierarchy<ActiveWeapon>();

        builder.RegisterEntryPoint<PlayerEntryPoint>();
        builder.RegisterEntryPoint<SwordControllerEntryPoint>();
    }
}