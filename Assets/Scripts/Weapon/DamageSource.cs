using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    private void  OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyHealth enemyHealth))
        {
            enemyHealth.TakeDamage(_damage);
        }
    }
}
