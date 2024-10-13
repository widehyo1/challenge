using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using VContainer;

public class PlayerMove : MonoBehaviour
{
    private PlayerInputListener playerInputListener;
    private PlayerData _playerData;
    private Rigidbody2D playerRigidBody;

    [Inject]
    public void Construct(PlayerData playerData)
    {
        Debug.Log($"Construction[PlayerMove] start");
        Debug.Log($"playerData: {playerData}");
        playerInputListener = GetComponent<PlayerInputListener>();
        _playerData = playerData;
        playerRigidBody = GetComponent<Rigidbody2D>();
        Debug.Log($"Construction[PlayerMove] end");
    }

    private void Update()
    {
        Vector2 movement = _playerData.speed * Time.deltaTime * playerInputListener.move;
        playerRigidBody.MovePosition(playerRigidBody.position + movement);
    }

}
