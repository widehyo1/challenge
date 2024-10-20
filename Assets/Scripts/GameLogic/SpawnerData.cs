using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPointData", menuName = "ScriptableObjects/SpawnPointData", order = 3)]
public class SpawnPointData : ScriptableObject
{
    [SerializeField]
    public List<Vector2> spawnPoints;
    public float scale = 1f;
}
