using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    public readonly string playerAddress = "Player";

    [SerializeField]
    public int maxHP = 100;
    [SerializeField]
    public int HP = 100;
    [SerializeField]
    public float speed = 15f;

}
