using VContainer;
using VContainer.Unity;

public class EnemyEntryPoint : IPostInitializable, IFixedTickable
{
    private readonly EnemyAI _ai;
    private readonly EnemyHealth _health;
    private readonly EnemyPathfinding _pathfinding;

    [Inject]
    public EnemyEntryPoint(EnemyAI ai, EnemyPathfinding pathfinding, EnemyHealth health)
    {
        _ai = ai;
        _pathfinding = pathfinding;
        _health = health;
    }

    public void FixedTick()
    {
        _pathfinding.FixedTick();
    }

    public void PostInitialize()
    {
        _ai.PostInitialize();
        _health.PostInitialize();
    }
}