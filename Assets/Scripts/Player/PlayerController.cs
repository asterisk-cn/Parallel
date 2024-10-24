using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerController : ITickable, IFixedTickable
{
    private static readonly int MoveSpeedHash = Animator.StringToHash("moveSpeed");
    private readonly Animator _animator;
    private readonly Camera _mainCamera;

    private readonly Transform _player;
    private readonly IPlayerInput _playerInput;
    private readonly Rigidbody2D _rb;
    private readonly SpriteRenderer _spriteRenderer;

    [Inject]
    public PlayerController(
        PlayerComponentProvider componentProvider,
        IPlayerInput playerInput)
    {
        _player = componentProvider.transform;
        _rb = componentProvider.Rigidbody2D;
        _animator = componentProvider.Animator;
        _spriteRenderer = componentProvider.SpriteRenderer;
        _mainCamera = componentProvider.MainCamera;
        _playerInput = playerInput;

        MoveSpeed = componentProvider.MoveSpeed;
    }

    private float MoveSpeed { get; }
    public bool FacingLeft { get; private set; }

    public void FixedTick()
    {
        Move();
        AdjustPlayerFacingDirection();
    }

    public void Tick()
    {
        _animator.SetFloat(MoveSpeedHash, _playerInput.Move.magnitude);
    }

    private void Move()
    {
        var movement = _playerInput.Move * MoveSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(_player.position + (Vector3)movement);
    }

    private void AdjustPlayerFacingDirection()
    {
        var playerScreenPoint = _mainCamera.WorldToScreenPoint(_player.position);

        FacingLeft = _playerInput.Look.x < playerScreenPoint.x;
        _spriteRenderer.flipX = FacingLeft;
    }
}