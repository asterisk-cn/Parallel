using VContainer.Unity;

public class SwordControllerEntryPoint : IPostInitializable, ITickable
{
    private readonly SwordController _swordController;

    public SwordControllerEntryPoint(SwordController swordController)
    {
        _swordController = swordController;
    }

    public void PostInitialize()
    {
        _swordController.PostInitialize();
    }

    public void Tick()
    {
        _swordController.Tick();
    }
}