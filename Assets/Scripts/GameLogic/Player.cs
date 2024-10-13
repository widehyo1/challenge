using UnityEngine;

public class Player : MonoBehaviour
{
    private IWeapon _weapon;
    private PlayerInputListener _playerInputListener;
    public PlayerInputListener playerInputListener {
        get => _playerInputListener;
        private set => _playerInputListener = value;
    }

    void Awake()
    {
        _playerInputListener = GetComponent<PlayerInputListener>();
    }

    void Start()
    {
        
    }

}
