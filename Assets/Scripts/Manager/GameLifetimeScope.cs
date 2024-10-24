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

        builder.RegisterComponentInHierarchy<PlayerComponentProvider>();
        builder.RegisterComponentInHierarchy<SwordComponentProvider>();
        builder.RegisterComponentInHierarchy<Sword>();
        builder.RegisterComponentInHierarchy<ActiveWeapon>();

        builder.RegisterEntryPoint<PlayerEntryPoint>();
        builder.RegisterEntryPoint<SwordControllerEntryPoint>();
    }
}