using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "ScriptableObjects/ProjectileData", order = 6)]
public class ProjectileData : ScriptableObject
{
    [SerializeField]
    public float speed;

    [SerializeField]
    public int defaultCapacity;

    [SerializeField]
    public int penetrationCount;

    [SerializeField]
    public int maxProjectileCountInScene;
}
