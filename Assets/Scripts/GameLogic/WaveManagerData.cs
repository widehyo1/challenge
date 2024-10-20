using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveManagerData", menuName = "ScriptableObjects/WaveManagerData", order = 9)]
public class WaveManagerData : ScriptableObject
{
    [SerializeField]
    public List<WaveData> waves;

}
