using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class SwordController : IPostInitializable, ITickable
{
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private readonly ActiveWeapon _activeWeapon;
    private readonly Animator _animator;
    private readonly Camera _mainCamera;

    private readonly Transform _player;
    private readonly PlayerController _playerController;
    private readonly IPlayerInput _playerInput;
    private readonly GameObject _slashAnimPrefab;
    private readonly Transform _slashAnimSpawnPoint;
    private readonly Transform _weaponCollider;

    private GameObject _slashAnim;
    private State _state;

    [Inject]
    public SwordController(
        SwordComponentProvider componentProvider,
        PlayerComponentProvider playerComponentProvider,
        PlayerController playerController,
        IPlayerInput playerInput)
    {
        _weaponCollider = componentProvider.WeaponCollider;
        _animator = componentProvider.Animator;
        _slashAnimPrefab = componentProvider.SlashAnimPrefab;
        _slashAnimSpawnPoint = componentProvider.SlashAnimSpawnPoint;
        _activeWeapon = componentProvider.ActiveWeapon;
        _mainCamera = componentProvider.MainCamera;
        _player = playerComponentProvider.transform;
        _playerController = playerController;
        _playerInput = playerInput;

        _state = State.Idle;
    }

    public void PostInitialize()
    {
        _playerInput.Attack
            .Where(_ => _state == State.Idle)
            .Subscribe(_ => Attack());
    }

    public void Tick()
    {
        MouseFollowWithOffset();
    }

    private void Attack()
    {
        _animator.SetTrigger(AttackHash);
        _weaponCollider.gameObject.SetActive(true);

        _slashAnim = Object.Instantiate(_slashAnimPrefab, _slashAnimSpawnPoint.position, _slashAnimSpawnPoint.rotation);
        _slashAnim.transform.parent = _slashAnimSpawnPoint;

        SwingFlipAnim();

        _state = State.Attacking;
    }

    private void SwingFlipAnim()
    {
        if (_playerController.FacingLeft)
        {
            _slashAnim.gameObject.transform.rotation *= Quaternion.Euler(0, 180, 0);
            _slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void OnAttackEnd()
    {
        _weaponCollider.gameObject.SetActive(false);

        _state = State.Idle;
    }

    private void MouseFollowWithOffset()
    {
        Vector2 playerScreenPosition = _mainCamera.WorldToScreenPoint(_player.position);

        var direction = _playerInput.Look - playerScreenPosition;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle is > 90 or < -90)
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(180, 0, -angle);
            _weaponCollider.rotation = Quaternion.Euler(180, 0, -angle);
        }
        else
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            _weaponCollider.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private enum State
    {
        Idle,
        Attacking
    }
}