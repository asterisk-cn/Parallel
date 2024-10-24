using VContainer.Unity;

public class PlayerEntryPoint : IPostInitializable, ITickable, IFixedTickable
{
    private readonly PlayerController _playerController;
    private readonly PlayerInputHandler _playerInput;

    public PlayerEntryPoint(
        PlayerController playerController,
        PlayerInputHandler playerInput
    )
    {
        _playerController = playerController;
        _playerInput = playerInput;
    }

    public void FixedTick()
    {
        _playerController.FixedTick();
    }

    public void PostInitialize()
    {
        _playerInput.PostInitialize();
    }

    public void Tick()
    {
        _playerController.Tick();
        _playerInput.Tick();
    }
}