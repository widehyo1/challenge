using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using VContainer;

public class PlayerMove : MonoBehaviour
{
    private PlayerInputListener playerInputListener;
    [SerializeField] public PlayerData playerData;
    private Rigidbody2D playerRigidBody;
    private Player player;

    void Awake()
    {
        Debug.Log($"Awake[PlayerMove] start");
        Debug.Log($"playerData: {playerData}");
        playerInputListener = GetComponent<PlayerInputListener>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
        Debug.Log($"Awake[PlayerMove] end");
    }

    private void FixedUpdate()
    {
        Vector2 move = playerInputListener.move;
        if (move != Vector2.zero)
        {
            player.lastMoveDirection = move.normalized;
        }
        Vector2 movement = playerData.speed * Time.deltaTime * move;
        playerRigidBody.MovePosition(playerRigidBody.position + movement);
    }

}
