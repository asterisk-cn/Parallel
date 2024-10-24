using UnityEngine;
using VContainer;

public class DamageSource : MonoBehaviour
{
    private int _damage;
    private PlayerMonoBehaviour _player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyHealthMonoBehaviour enemyHealth))
            enemyHealth.TakeDamage(_damage, _player.transform);
    }

    [Inject]
    public void Construct(PlayerMonoBehaviour player, PlayerComponentProvider playerComponentProvider)
    {
        _player = player;

        _damage = playerComponentProvider.AttackDamage;
    }
}