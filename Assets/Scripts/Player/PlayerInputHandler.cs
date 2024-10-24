using R3;
using UnityEngine;
using VContainer.Unity;

public class PlayerInputHandler : IPlayerInput, IPostInitializable, ITickable
{
    private readonly PlayerControls _playerControls;

    public PlayerInputHandler()
    {
        _playerControls = new PlayerControls();
        _playerControls.Enable();
    }

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }
    public Subject<Unit> Attack { get; private set; }

    public void PostInitialize()
    {
        Attack = new Subject<Unit>();
        _playerControls.Combat.Attack.started += _ => Attack.OnNext(Unit.Default);
    }

    public void Tick()
    {
        Move = _playerControls.Movement.Move.ReadValue<Vector2>();
        Look = _playerControls.Movement.Look.ReadValue<Vector2>();
    }
}