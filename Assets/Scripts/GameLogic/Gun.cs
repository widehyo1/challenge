using System.IO;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;

public class Gun : MonoBehaviour, IWeapon
{
    private GameObject player;
    private PlayerInputListener playerInputListener;
    private WeaponData _gunData;
    private ProjectileData _bulletData;
    private float lastFireTime;
    private float timeBetFire;
    private Bullet _bullet;
    private IObjectPool<IProjectile> _bulletPool;
    private Vector2 lastFireDirection;
    public bool IsInitialized { get; private set; }

    [Inject]
    public void Construct(WeaponData gunData, Bullet bullet)
    {
        Debug.Log($"Construction[Gun] start");
        Debug.Log($"gunData: {gunData}");
        Debug.Log($"bullet: {bullet}");
        player = GameObject.FindWithTag("Player");
        playerInputListener = player.GetComponent<Player>().playerInputListener;
        _gunData = gunData;
        _bullet = bullet;
        _bulletData = bullet.bulletData;
        lastFireTime = gunData.lastFireTime;
        timeBetFire = gunData.timeBetFire;
        lastFireDirection = Vector2.up;
        Debug.Log($"Construction[Gun] end");
    }
    public void Fire()
    {
        Bullet pooledBullet = _bulletPool.Get() as Bullet;
        pooledBullet.transform.SetPositionAndRotation(player.transform.position, Quaternion.identity);
        if (playerInputListener.move != Vector2.zero)
        {
            pooledBullet.bulletRigidBody.velocity = playerInputListener.move.normalized * _bulletData.speed;
        }
        else
        {
            pooledBullet.bulletRigidBody.velocity = lastFireDirection * _bulletData.speed;
        }
    }

    public bool Performable(float timeBetFire)
    {
        // Debug.Log(_bulletPool.CountInactive);
        // return Time.time >= timeBetFire + lastFireTime && _bulletPool.CountInactive > 0;
        return Time.time >= timeBetFire + lastFireTime;
    }

    // Start is called before the first frame update
    async void Start()
    {
        Debug.Log("Start[Gun] start");
        bool isBulletReady = await _bullet.Ready();
        Debug.Log($"isBulletReady: {isBulletReady}");
        _bulletPool = _bullet.IBulletPool;
        Debug.Log($"_bulletPool: {_bulletPool}");
        Debug.Log("Start[Gun] end");
        IsInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInitialized)
        {
            if (Performable(timeBetFire))
            {
                Fire();
            }
        }
    }
}
