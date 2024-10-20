using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHP;
    public int HP;
    private Player player;

    void Awake()
    {
        Debug.Log($"Awake[PlayerHealth] start");
        player = GetComponent<Player>();
        Debug.Log($"player: {player}");
        Debug.Log($"Awake[PlayerHealth] end");
    }

    public void OnDamage(int damage, System.Numerics.Vector2 hitPoint, System.Numerics.Vector2 hitNormal)
    {
        Debug.Log($"OnDamage[PlayerHealth] with damage: {damage}");
        HP = Mathf.Max(0, HP - damage);
        if (HP == 0)
        {
            player.OnDie();
        }
    }
}
