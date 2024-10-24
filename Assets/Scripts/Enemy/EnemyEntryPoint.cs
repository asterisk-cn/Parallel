using VContainer;
using VContainer.Unity;

public class EnemyEntryPoint : IPostInitializable, IFixedTickable
{
    private readonly EnemyAI _ai;
    private readonly Flash _flash;
    private readonly EnemyHealth _health;
    private readonly KnockBack _knockBack;
    private readonly EnemyPathfinding _pathfinding;

    [Inject]
    public EnemyEntryPoint(
        EnemyAI ai,
        EnemyPathfinding pathfinding,
        EnemyHealth health,
        KnockBack knockBack,
        Flash flash
    )
    {
        _ai = ai;
        _pathfinding = pathfinding;
        _health = health;
        _knockBack = knockBack;
        _flash = flash;
    }

    public void FixedTick()
    {
        _pathfinding.FixedTick();
    }

    public void PostInitialize()
    {
        _ai.PostInitialize();
        _health.PostInitialize();
        _flash.PostInitialize();
        _knockBack.PostInitialize();
    }
}