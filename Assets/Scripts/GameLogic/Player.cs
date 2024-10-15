using UnityEngine;
using VContainer;

public class Player : MonoBehaviour
{
    private IWeapon _weapon;
    private PlayerMove _playerMove;
    public PlayerMove playerMove {
        get => _playerMove;
        set => _playerMove = value;
    }
    private PlayerInputListener _playerInputListener;
    public PlayerInputListener playerInputListener {
        get => _playerInputListener;
        private set => _playerInputListener = value;
    }

    public Vector2 lastMoveDirection;

    public void SetWeapon(IWeapon weapon)
    {
        _weapon = weapon;
    }

    void Awake()
    {
        Debug.Log("Awake[Player] start");
        _playerInputListener = GetComponent<PlayerInputListener>();
        Debug.Log("_playerInputListener is set");
        playerMove = GetComponent<PlayerMove>();
        lastMoveDirection = Vector2.up;
        Debug.Log("Awake[Player] end");
    }

    void Start()
    {
        Debug.Log("Start[Player] start");
        Debug.Log($"_weapon: {_weapon}");
        Debug.Log($"_weapon as Gun: {_weapon as Gun}");
        Debug.Log("Start[Player] end");
    }

}
