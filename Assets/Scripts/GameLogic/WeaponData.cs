using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 5)]
public class WeaponData : ScriptableObject
{

    [SerializeField]
    public string weaponAddress;

    [SerializeField]
    public int damage;
    [SerializeField]
    public float timeBetFire;
    [SerializeField]
    public float lastFireTime;
}
