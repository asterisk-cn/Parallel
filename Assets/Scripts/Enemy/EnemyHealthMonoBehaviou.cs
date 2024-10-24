using UnityEngine;
using VContainer;

public class EnemyHealthMonoBehaviour : MonoBehaviour
{
    private EnemyHealth _impl;

    private void OnDisable()
    {
        _impl = null;
    }

    [Inject]
    public void Construct(EnemyHealth impl)
    {
        _impl = impl;
    }

    public void TakeDamage(int damage, Transform damageSource)
    {
        _impl.TakeDamage(damage, damageSource);
    }
}