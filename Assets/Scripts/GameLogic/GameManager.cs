using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

public class GameManager : MonoBehaviour
{
    // [Inject] private readonly IObjectResolver resolver;
    [Inject] IObjectResolver _resolver;
    private Dictionary<string, GameObject> _prefabInventory;
    private IProjectilePool bulletPool;
    private IObjectPool<IProjectile> _bulletPool;
    private PrefabLoader _prefabLoader;
    private Player _player;

    async void Start()
    {
        Debug.Log("Start[GameManager] start");
        _prefabLoader = _resolver.Resolve<PrefabLoader>();
        Debug.Log("Waiting prefab loader is ready");
        await UniTask.WaitUntil(() => _prefabLoader.IsInitialized);
        Debug.Log($"_prefabLoader.PrefabInventory: {_prefabLoader.PrefabInventory}");
        _prefabInventory = _prefabLoader.PrefabInventory;
        bulletPool = _resolver.Resolve<BulletPool>();
        _prefabInventory.TryGetValue("Bullet", out GameObject bulletPrefab);
        bulletPool.SetPrefab(bulletPrefab);
        _bulletPool = bulletPool.GetProjectilePool();
        Debug.Log("Start[GameManager] end");

        OnGameStart();
    }

    public void OnGameStart()
    {
        Debug.Log("OnGameStart[GameManager] start");
        _prefabInventory.TryGetValue("Player", out GameObject playerPrefab);
        GameObject player = Instantiate(playerPrefab);
        _player = player.GetComponent<Player>();
        IWeapon Igun = _resolver.Resolve<IWeapon>();
        Gun gun = Igun as Gun;
        gun.SetBulletPool(bulletPool);
        _player.SetWeapon(gun);
        bulletPool.SetPlayer(_player);
        Debug.Log("OnGameStart[GameManager] end");
    }

    [Inject]
    public void Construct(IObjectResolver resolver)
    {
        Debug.Log($"Construction[GameManager] start");
        _resolver = resolver;
        Debug.Log("Construction[GameManager] ended");
    }

}
