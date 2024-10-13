using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;

public class Bullet : MonoBehaviour, IProjectile
{
    private BulletPool _bulletPool;
    private IObjectPool<IProjectile> _IbulletPool;
    public IObjectPool<IProjectile> IBulletPool { get => _IbulletPool; set => _IbulletPool = value; }
    private GameData _gameData;
    private ProjectileData _bulletData;
    public ProjectileData bulletData { get => _bulletData; private set => _bulletData = value; }
    public Rigidbody2D bulletRigidBody;
    public bool IsInitialized { get; private set; }

    [Inject]
    public void Construct(BulletPool bulletPool, GameData gameData, ProjectileData bulletData)
    {
        Debug.Log($"Construction[Bullet] start");
        Debug.Log($"bulletPool: {bulletPool}");
        Debug.Log($"gameData: {gameData}");
        _bulletPool = bulletPool;
        Debug.Log($"_bulletPool: {_bulletPool}");
        _gameData = gameData;
        Debug.Log($"_gameData: {_gameData}");
        _bulletData = bulletData;
        Debug.Log($"_bulletData: {_bulletData}");
        bulletRigidBody = GetComponent<Rigidbody2D>();
        Debug.Log($"bulletRigidBody: {bulletRigidBody}");
        Debug.Log($"Construction[Bullet] ended");
    }

    public bool InGameBoard(GameData gameData)
    {
        throw new System.NotImplementedException();
    }

    public void InvokeRelease()
    {
        throw new System.NotImplementedException();
    }

    async public UniTask<bool> Ready()
    {
        Debug.Log("Ready[Bullet] start");
        // await UniTask.WaitUntil(() => _bulletPool.IsInitialized);
        // bool result = await _bulletPool.Ready();
        await _bulletPool.Ready();
        Debug.Log("_bulletPool.IsInitialized");
        _IbulletPool = _bulletPool.GetProjectilePool();
        Debug.Log($"_IbulletPool: {_IbulletPool}");
        Debug.Log("Ready[Bullet] end");
        return _IbulletPool != null;
    }

}
