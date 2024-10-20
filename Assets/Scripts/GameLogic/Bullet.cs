using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;

public class Bullet : MonoBehaviour, IProjectile
{
    private IObjectPool<IProjectile> _IbulletPool;
    public IObjectPool<IProjectile> IbulletPool {
        get => _IbulletPool;
        set => _IbulletPool = value;
    }
    private Rigidbody2D _bulletRigidBody;
    public Rigidbody2D bulletRigidBody {
        get => _bulletRigidBody;
    }
    [SerializeField] private  GameData _gameData;
    private GameObject _playerObj;
    public GameObject playerObj {
        get => _playerObj;
        set => _playerObj = value;
    }
    private bool isAfterStart = false;
    private Player _player;
    public Player player {
        get => _player;
    }
    [SerializeField] private ProjectileData _bulletData;
    public ProjectileData bulletData {
        get => _bulletData;
        set => _bulletData = value;
    }

    [Inject]
    public void Construct(GameData gameData)
    {
        Debug.Log($"Construct[Bullet] start");
        _gameData = gameData;
        Debug.Log($"Construct[Bullet] end");
    }

    void Awake()
    {
        Debug.Log($"Awake[Bullet] start");
        _bulletRigidBody = GetComponent<Rigidbody2D>();
        Debug.Log($"_bulletRigidBody: {_bulletRigidBody}");
        Debug.Log($"Awake[Bullet] ended");
    }

    void OnEnable()
    {
        if (!isAfterStart) return;
        Debug.Log($"OnEnabled[Bullet] start");
        Debug.Log($"_player.lastMoveDirection: {_player.lastMoveDirection}");
        Debug.Log($"_bulletData.speed: {_bulletData.speed}");
        SetPositionAndVelocity();
        Debug.Log($"OnEnabled[Bullet] ended");
    }

    void Start()
    {
        Debug.Log($"Start[Bullet] start");
        playerObj = GameObject.FindWithTag("Player");
        _player = playerObj.GetComponent<Player>();
        isAfterStart = true;
        Debug.Log($"Start[Bullet] ended");
    }

    private void SetPlayer()
    {
        if (_player == null)
        {
            playerObj = GameObject.FindWithTag("Player");
            _player = playerObj.GetComponent<Player>();
        }
    }

    public void SetPositionAndVelocity()
    {
        SetPlayer();
        transform.SetPositionAndRotation(playerObj.transform.position, Quaternion.identity);
        _bulletRigidBody.velocity = _player.lastMoveDirection * _bulletData.speed;
    }

    public void SetPositionOrigin()
    {
        transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    public void StopMovement()
    {
        _bulletRigidBody.velocity = Vector2.zero;
    }

    public void FixedUpdate()
    {
        if (!InGameBoard())
        {
            InvokeRelease();
        }
    }

    void OnDisable()
    {
        SetPositionOrigin();
        StopMovement();
    }

    private Vector2 VectorFromPlayer()
    {
        return transform.position - playerObj.transform.position;
    }

    private bool IsHorizontalPositionOnGameBoard()
    {
        return Mathf.Abs(VectorFromPlayer().x) <= Mathf.Abs(_gameData.gameBoardleftTop.x);
    }

    private bool IsVerticalPositionOnGameBoard()
    {
        return Mathf.Abs(VectorFromPlayer().y) <= Mathf.Abs(_gameData.gameBoardleftTop.y);
    }

    public bool InGameBoard()
    {
        return IsHorizontalPositionOnGameBoard() && IsVerticalPositionOnGameBoard();
    }

    public void InvokeRelease()
    {
        _IbulletPool.Release(this);
    }

    public bool IsReset()
    {
        return true;
    }

}
