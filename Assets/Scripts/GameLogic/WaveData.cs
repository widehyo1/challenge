using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "WaveData", menuName = "ScriptableObjects/WaveData", order = 8)]
public class WaveData : ScriptableObject
{
    [SerializeField]
    public List<Composition> compositions;

}

[Serializable]
public class Composition
{
    [SerializeField]
    public string enermyName;
    [SerializeField]
    public int maxCount;
}
