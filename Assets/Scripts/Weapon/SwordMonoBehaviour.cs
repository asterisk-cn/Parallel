using UnityEngine;
using VContainer;

public class SwordMonoBehaviour : MonoBehaviour
{
    private SwordController _swordController;

    [Inject]
    public void Construct(SwordController swordController)
    {
        _swordController = swordController;
    }

    public void OnAttackEnd()
    {
        _swordController.OnAttackEnd();
    }
}