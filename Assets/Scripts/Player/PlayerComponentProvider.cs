using UnityEngine;
using VContainer;

public class PlayerComponentProvider : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    public Rigidbody2D Rigidbody2D { get; private set; }
    public Animator Animator { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Camera MainCamera { get; private set; }
    public float MoveSpeed => _moveSpeed;

    [Inject]
    public void Construct()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        MainCamera = Camera.main;
    }
}