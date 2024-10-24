using UnityEngine;
using VContainer;

public class EnemyHealthMonoBehaviour : MonoBehaviour
{
    private EnemyHealth _impl;

    private void OnDisable()
    {
        _impl.Dispose();
        _impl = null;
    }

    [Inject]
    public void Construct(EnemyHealth impl)
    {
        _impl = impl;
    }

    public void TakeDamage(int damage)
    {
        _impl.TakeDamage(damage);
    }
}