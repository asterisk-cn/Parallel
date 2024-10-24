using UnityEngine;
using VContainer;

public class PlayerComponentProvider : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private int _attackDamage = 1;
    public Rigidbody2D Rigidbody2D { get; private set; }
    public Animator Animator { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Camera MainCamera { get; private set; }
    public float MoveSpeed => _moveSpeed;
    public int AttackDamage => _attackDamage;

    [Inject]
    public void Construct()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        MainCamera = Camera.main;
    }
}