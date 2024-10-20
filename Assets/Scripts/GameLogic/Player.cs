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
    private bool _isDead = false;
    public bool isDead {
        get => _isDead;
    }

    private PlayerHealth playerHealth;
    public Vector2 lastMoveDirection;

    [Inject]
    // public void Construct(IWeapon weapon)
    public void Construct()
    {
        Debug.Log("Construct[Palyer] start");
        // _weapon = weapon;
        // Debug.Log($"_weapon: {_weapon}");
        Debug.Log("Construct[Palyer] end");
    }

    void Awake()
    {
        Debug.Log("Awake[Player] start");
        _playerInputListener = GetComponent<PlayerInputListener>();
        Debug.Log("_playerInputListener is set");
        playerMove = GetComponent<PlayerMove>();
        playerHealth = GetComponent<PlayerHealth>();
        lastMoveDirection = Vector2.up;
        Debug.Log("Awake[Player] end");
    }

    void Start()
    {
        Debug.Log("Start[Player] start");
        Debug.Log($"_weapon: {_weapon}");
        // Debug.Log($"_weapon as Gun: {_weapon as Gun}");
        Debug.Log("Start[Player] end");
    }

    public void SetWeapon(IWeapon weapon)
    {
        _weapon = weapon;
    }
    public void OnDie()
    {
        Debug.Log("OnDie[Player]");
        _isDead = true;
    }

    public override string ToString()
    {
        return $"({playerHealth.HP}/{playerHealth.maxHP}, speed: {playerMove.playerData.speed})";
    }
}
