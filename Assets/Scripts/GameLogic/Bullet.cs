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

    private GameObject playerObj;
    private Player player;
    [SerializeField] private ProjectileData _bulletData;
    public ProjectileData bulletData {
        get => _bulletData;
        set => _bulletData = value;
    }
    void Awake()
    {
        Debug.Log($"Awake[Bullet] start");
        _bulletRigidBody = GetComponent<Rigidbody2D>();
        playerObj = GameObject.FindWithTag("Player");
        player = playerObj.GetComponent<Player>();
        Debug.Log($"_bulletRigidBody: {_bulletRigidBody}");
        Debug.Log($"Awake[Bullet] ended");
    }

    void OnEnable()
    {
        Debug.Log($"OnEnabled[Bullet] start");
        // Debug.Log($"player.lastMoveDirection: {player.lastMoveDirection}");
        // Debug.Log($"_bulletData.speed: {_bulletData.speed}");
        // Vector2 movement = _bulletData.speed * Time.deltaTime * player.lastMoveDirection;
        // _bulletRigidBody.velocity = movement;
        // transform.SetPositionAndRotation(player.transform.position, Quaternion.identity);
        // bulletRigidBody.velocity = player.lastMoveDirection * bulletData.speed;
        Debug.Log($"OnEnabled[Bullet] ended");
    }

    public bool InGameBoard(GameData gameData)
    {
        Collider2D[] colliders = null;
        Physics2D.OverlapBoxNonAlloc(playerObj.transform.position, size:gameData.gameBoardleftTop, angle:0f, results:colliders, layerMask:7);
        return colliders.Length > 0;
    }

    public void InvokeRelease()
    {
        _IbulletPool.Release(this);
    }

}
