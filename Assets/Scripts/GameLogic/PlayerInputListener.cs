using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputListener : MonoBehaviour
{
    private PlayerInput playerInputActions;
    private InputAction moveAction;
    private Vector2 _move;

    public Vector2 move {
        get => _move;
        private set => _move = value;
    }

    void Awake()
    {
        playerInputActions = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        moveAction = playerInputActions.actions.FindAction("Player/Move");
    }

    public void OnMove()
    {
        _move = moveAction.ReadValue<Vector2>();
    }

}
