using UnityEngine;
using VContainer;

public class EnemyComponentProvider : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private int _maxHealth = 3;

    public Rigidbody2D Rigidbody2D { get; private set; }
    public float MoveSpeed => _moveSpeed;
    public int MaxHealth => _maxHealth;

    [Inject]
    public void Construct()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
}