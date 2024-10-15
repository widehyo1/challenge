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
    private GameObject _bulletPrefab;
    public GameObject bulletPrefab {
        get => _bulletPrefab;
        set => _bulletPrefab = value;
    }
    private ProjectileData _bulletData;
    private CancellationTokenSource cts;
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] public bool IsInitialized { get; private set; }

    private Player _player;
    public void SetPlayer(Player player)
    {
        _player = player;
    }

    [Inject]
    public void Construct(ProjectileData bulletData)
    {
        Debug.Log($"Construction[BulletPool] start");
        Debug.Log($"bulletData: {bulletData}");
        _bulletData = bulletData;
        Debug.Log($"_bulletData: {_bulletData}");
        cts = new CancellationTokenSource();
        Debug.Log("Construction[BulletPool] ended");
    }

    public IObjectPool<IProjectile> GetProjectilePool()
    {
        Debug.Log("GetProjectilePool start");
        
        IbulletPool = new ObjectPool<IProjectile>(CreateBullet, OnGetFromPool,
            OnReleaseToPool, OnDestroyPooledObject,
            collectionCheck, _bulletData.defaultCapacity, _bulletData.maxSize);
        return IbulletPool;
    }

    private Bullet CreateBullet()
    {
        GameObject bulletInstance = Instantiate(_bulletPrefab,
            Vector3.zero, Quaternion.identity);
        Bullet bullet = bulletInstance.GetComponent<Bullet>();
        bullet.IbulletPool = IbulletPool;
        return bullet;
    }

    private void OnGetFromPool(IProjectile projectile)
    {
        if (projectile is Bullet bullet)
        {
            bullet.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("projectile is not a bullet");
        }
    }

    private void OnReleaseToPool(IProjectile projectile)
    {
        if (projectile is Bullet bullet)
        {
            bullet.gameObject.SetActive(false);
            bullet.gameObject.transform.SetPositionAndRotation(_player.gameObject.transform.position, Quaternion.identity);
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

    public void SetPrefab(GameObject bulletPrefab)
    {
        _bulletPrefab = bulletPrefab;
    }
}
