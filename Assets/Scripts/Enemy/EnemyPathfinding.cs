using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;

    private Rigidbody2D _rb;
    private Vector2 _moveDirection;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _moveDirection * (_moveSpeed * Time.fixedDeltaTime));
    }
    
    public void MoveTo(Vector2 targetPosition)
    {
        _moveDirection = (targetPosition - (Vector2)transform.position).normalized;
    }
}
