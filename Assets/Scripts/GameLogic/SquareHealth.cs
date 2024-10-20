using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class SquareHealth : MonoBehaviour, IDamageable
{
    public int maxHP;
    public int HP;
    private Square square;

    [Inject]
    public void Construct(EnermyData squareData)
    {
        Debug.Log($"Construction[SquareHealth] start");

        Debug.Log($"Construction[SquareHealth] end");
    }

    void Awake()
    {
        Debug.Log($"Awake[SquareHealth] start");
        square = GetComponent<Square>();
        Debug.Log($"square: {square}");
        Debug.Log($"Awake[SquareHealth] end");
    }

    public void OnDamage(int damage, System.Numerics.Vector2 hitPoint, System.Numerics.Vector2 hitNormal)
    {
        Debug.Log($"OnDamage[SquareHealth] with damage: {damage}");
        HP = Mathf.Max(0, HP - damage);
        if (HP == 0)
        {
            square.OnDie();
        }
    }
}
