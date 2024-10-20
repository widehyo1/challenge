using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnermyData", menuName = "ScriptableObjects/EnermyData", order = 7)]
public class EnermyData : ScriptableObject
{
    [SerializeField]
    public int maxHP;

    [SerializeField]
    public int HP;

    [SerializeField]
    public float speed;

    [SerializeField]
    public int damage;

    [SerializeField]
    public float timeBetAttack;
}
