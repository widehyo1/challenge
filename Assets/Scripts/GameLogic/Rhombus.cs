using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class Rhombus : MonoBehaviour
{

    private int maxHP;
    private int HP;
    private int damage;
    private float speed;
    private float timeBetAttack;

    [Inject]
    // public void Construct(IWeapon weapon)
    public void Construct(EnermyData rhombusData)
    {
        Debug.Log("Construct[Palyer] start");
        // _weapon = weapon;
        // Debug.Log($"_weapon: {_weapon}");
        Debug.Log("Construct[Palyer] end");
    }
}
