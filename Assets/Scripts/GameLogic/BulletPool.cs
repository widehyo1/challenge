using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

public class BulletPool : MonoBehaviour, IProjectilePool
{
    private IObjectPool<IProjectile> IbulletPool;
    private Bullet _bulletPrefab;
    private ProjectileData _bulletData;
    private GameData _gameData;
    public int defaultCapacity;
    public int maxSize;
    private CancellationTokenSource cts;
    private GameObject _playerObj;
    private Player _player;
    [Inject]
    public void Construct(ProjectileData bulletData, Bullet bulletPrefab, GameData gameData)
    {
        Debug.Log($"Construction[BulletPool] start");
        Debug.Log($"bulletData: {bulletData}");
        _bulletData = bulletData;
        _gameData = gameData;
        _bulletPrefab = bulletPrefab;
        defaultCapacity = bulletData.defaultCapacity;
        maxSize = bulletData.maxProjectileCountInScene;
        Debug.Log($"_bulletData: {_bulletData}");
        cts = new CancellationTokenSource();
        Debug.Log("Construction[BulletPool] ended");
    }

    public void Start ()
    {
        Debug.Log($"Start[BulletPool] start");
        _playerObj = GameObject.FindWithTag("Player");
        _player = _playerObj.GetComponent<Player>();
        IbulletPool = new ObjectPool<IProjectile>(CreateBullet, OnGetFromPool,
            OnReleaseToPool, OnDestroyPooledObject);
        Debug.Log($"Start[BulletPool] end");
    }

    public IObjectPool<IProjectile> GetProjectilePool()
    {
        return IbulletPool;
    }

    private Bullet CreateBullet()
    {
        Bullet bulletInstance = Instantiate(_bulletPrefab,
            Vector3.zero, Quaternion.identity);
        bulletInstance.IbulletPool = IbulletPool;
        return bulletInstance;
    }

    private void OnGetFromPool(IProjectile projectile)
    {
        Bullet bullet = projectile as Bullet;
        if (bullet.gameObject.activeInHierarchy && bullet.IsReset())
        {
            bullet.SetPositionAndVelocity();
        }
        if (!bullet.gameObject.activeInHierarchy)
        {
            bullet.gameObject.SetActive(true);
        }
    }

    private void OnReleaseToPool(IProjectile projectile)
    {
        if (projectile is Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("projectile is not a bullet");
        }
    }

    private void OnDestroyPooledObject(IProjectile projectile)
    {
        if (projectile is Bullet bullet)
        {
            Destroy(bullet.gameObject);
        }
        else
        {
            Debug.Log("projectile is not a bullet");
        }
    }

    public IProjectile Get()
    {
        return IbulletPool.Get();
    }

    public int GetCountInactive()
    {
        return IbulletPool.CountInactive;
    }
}
