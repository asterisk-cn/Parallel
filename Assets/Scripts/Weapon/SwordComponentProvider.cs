using UnityEngine;
using VContainer;

public class SwordComponentProvider : MonoBehaviour
{
    [SerializeField] private GameObject _slashAnimPrefab;
    [SerializeField] private Transform _slashAnimSpawnPoint;
    [SerializeField] private Transform _weaponCollider;
    public Animator Animator { get; private set; }
    public Transform WeaponCollider => _weaponCollider;
    public GameObject SlashAnimPrefab => _slashAnimPrefab;
    public Transform SlashAnimSpawnPoint => _slashAnimSpawnPoint;
    public ActiveWeapon ActiveWeapon { get; private set; }
    public Camera MainCamera { get; private set; }

    [Inject]
    public void Construct(ActiveWeapon activeWeapon)
    {
        ActiveWeapon = activeWeapon;

        Animator = GetComponent<Animator>();
        MainCamera = Camera.main;
    }
}