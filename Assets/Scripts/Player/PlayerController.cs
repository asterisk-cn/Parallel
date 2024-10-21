using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft { get; set; }
    
    [SerializeField] private float moveSpeed = 1f;
    
    private static readonly int MoveSpeed = Animator.StringToHash("moveSpeed");
    private PlayerControls _playerControls;
    private Vector2 _movement;
    private Vector2 _lookPosition;
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Camera _camera;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _camera = Camera.main;
    }
    
    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }
    
    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }

    private void PlayerInput()
    {
        _movement = _playerControls.Movement.Move.ReadValue<Vector2>();
        _lookPosition = _playerControls.Movement.Look.ReadValue<Vector2>();

        _animator.SetFloat(MoveSpeed, _movement.magnitude);
    }

    private void Move()
    {
        _rb.MovePosition(_rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 playerScreenPoint = _camera.WorldToScreenPoint(transform.position);

        if (_lookPosition.x < playerScreenPoint.x)
        {
            _spriteRenderer.flipX = true;
            FacingLeft = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
            FacingLeft = false;
        }
    }
}
