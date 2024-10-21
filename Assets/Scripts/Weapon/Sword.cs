using System;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject _slashAnimPrefab;
    [SerializeField] private Transform _slashAnimSpawnPoint;

    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private PlayerControls _playerControls;
    private Animator _animator;
    private PlayerController _playerController;
    private ActiveWeapon _activeWeapon;
    private Camera _mainCamera;
    private GameObject _slashAnim;
    private State _state;

    private enum State
    {
        Idle,
        Attacking
    }

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _animator = GetComponent<Animator>();
        _playerController = GetComponentInParent<PlayerController>();
        _activeWeapon = GetComponentInParent<ActiveWeapon>();
        _mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Start()
    {
        _playerControls.Combat.Attack.started += _ =>
        {
            if (_state == State.Idle)
            {
                Attack();
            }
        };
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    private void Attack()
    {
        _animator.SetTrigger(AttackHash);

        _slashAnim = Instantiate(_slashAnimPrefab, _slashAnimSpawnPoint.position, _slashAnimSpawnPoint.rotation);
        _slashAnim.transform.parent = transform.parent.parent;

        _state = State.Attacking;
    }

    public void SwingFlipAnim()
    {
        if (_playerController.FacingLeft)
        {
            _slashAnim.gameObject.transform.rotation *= Quaternion.Euler(0, 180, 0);
            _slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void OnAttackEnd()
    {
        _state = State.Idle;
    }

    private void MouseFollowWithOffset()
    {
        var mousePosition = _playerControls.Movement.Look.ReadValue<Vector2>();
        Vector2 playerScreenPosition = _mainCamera.WorldToScreenPoint(transform.parent.position);

        var direction = mousePosition - playerScreenPosition;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle is > 90 or < -90)
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(180, 0, -angle);
        }
        else
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}