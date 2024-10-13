using UnityEngine;

[CreateAssetMenu(fileName = "WaveInfoData", menuName = "ScriptableObjects/WaveInfoData", order = 4)]
public class WaveInfoData : ScriptableObject
{
    [SerializeField]
    public readonly string playerAddress = "Player";

    [SerializeField]
    public int maxHP = 100;
    [SerializeField]
    public float speed = 10f;

}
