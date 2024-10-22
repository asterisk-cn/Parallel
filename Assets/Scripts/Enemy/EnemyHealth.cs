using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 3;
    
    private int _currentHealth;
    
    private void Awake()
    {
        _currentHealth = _maxHealth;
    }
    
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        DetectDeath();
    }
    
    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
