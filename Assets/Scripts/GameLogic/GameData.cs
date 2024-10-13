using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 3)]
public class GameData : ScriptableObject
{
    [SerializeField]
    public Vector2 gameBoardleftTop;

    [SerializeField]
    public Vector2 gameBoardRightBottom;

    [SerializeField]
    public List<string> prefabAddresses;
}
