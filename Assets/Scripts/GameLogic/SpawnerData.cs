using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnerData", menuName = "ScriptableObjects/SpawnerData", order = 3)]
public class SpawnerData : ScriptableObject
{
    [SerializeField]
    public List<Vector2> spawnPoints;
    public float scale = 1f;
}
