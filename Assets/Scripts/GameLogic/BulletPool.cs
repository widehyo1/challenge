using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

public class BulletPool : MonoBehaviour, IProjectilePool
{
    private IObjectPool<IProjectile> IbulletPool;
    private PrefabLoader _prefabLoader;
    private GameObject _bulletPrefab;
    private ProjectileData _bulletData;
    private CancellationTokenSource cts;
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] public bool IsInitialized { get; private set; }

    [Inject]
    public void Construct(PrefabLoader prefabLoader, ProjectileData bulletData)
    {
        Debug.Log($"Construction[BulletPool] start");
        Debug.Log($"prefabLoader: {prefabLoader}");
        Debug.Log($"bulletData: {bulletData}");
        _prefabLoader = prefabLoader;
        Debug.Log($"_prefabLoader: {_prefabLoader}");
        _bulletData = bulletData;
        Debug.Log($"_bulletData: {_bulletData}");
        cts = new CancellationTokenSource();
        Debug.Log("Construction[BulletPool] ended");
    }

    async public UniTask<bool> Ready()
    {
        Debug.Log("Ready[BulletPool] start");
        await UniTask.WaitUntil(() => PrefabLoader.IsInitialized);
        Debug.Log("prefabLoader.IsInitialized");
        PrefabLoader.prefabInventory.TryGetValue("Bullet", out GameObject bulletPrefab);
        _bulletPrefab = bulletPrefab;
        Debug.Log($"_bulletPrefab: {_bulletPrefab}");
        IsInitialized = true;
        Debug.Log("Ready[BulletPool] end");
        return IsInitialized;
    }

    public IObjectPool<IProjectile> GetProjectilePool()
    {
        IbulletPool = new ObjectPool<IProjectile>(CreateBullet, OnGetFromPool,
            OnReleaseToPool, OnDestroyPooledObject,
            collectionCheck, _bulletData.defaultCapacity, _bulletData.maxSize);
        return IbulletPool;
    }

    private Bullet CreateBullet()
    {
        GameObject bulletInstance = Instantiate(_bulletPrefab,
            Vector3.zero, Quaternion.identity);
        _ = bulletInstance.TryGetComponent<Bullet>(out Bullet bullet);
        bullet.IBulletPool = IbulletPool;
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
}
