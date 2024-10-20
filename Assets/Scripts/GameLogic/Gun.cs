using System.IO;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;

public class Gun : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponData _gunData;
    public WeaponData gunData {
        get => _gunData;
        set => _gunData = value;
    }
    [SerializeField] private ProjectileData _bulletData;
    public ProjectileData bulletData {
        get => _bulletData;
        set => _bulletData = value;
    }
    private float lastFireTime;
    private float timeBetFire;
    private IProjectilePool _bulletPool;
    public IProjectilePool bulletPool {
        get => _bulletPool;
        set => _bulletPool = value;
    }
    private Vector2 lastFireDirection;
    private Player player;


    [Inject]
    public void Construct(WeaponData gunData, ProjectileData bulletData, BulletPool bulletPool)
    {
        Debug.Log($"Construction[Gun] start");
        Debug.Log($"gunData: {gunData}");
        _gunData = gunData;
        _bulletData = bulletData;
        _bulletPool = bulletPool;
        Debug.Log($"Construction[Gun] end");
    }

    public void Awake()
    {
        Debug.Log($"Awake[Gun] start");
        lastFireTime = gunData.lastFireTime;
        timeBetFire = gunData.timeBetFire;
        lastFireDirection = Vector2.up;
        Debug.Log($"Awake[Gun] start");
    }

    public void Start()
    {
        Debug.Log($"Start[Gun] start");
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        Debug.Log($"Start[Gun] end");
    }

    public void Fire()
    {
        lastFireTime = Time.time;
        Bullet pooledBullet = _bulletPool.Get() as Bullet;
        pooledBullet.transform.SetPositionAndRotation(pooledBullet.transform.position, Quaternion.identity);
        if (player.playerInputListener.move != Vector2.zero)
        {
            pooledBullet.bulletRigidBody.velocity = player.playerInputListener.move.normalized * _bulletData.speed;
            lastFireDirection = player.playerInputListener.move.normalized;
        }
        else
        {
            pooledBullet.bulletRigidBody.velocity = lastFireDirection * _bulletData.speed;
        }
    }

    public bool Performable(float timeBetFire)
    {
        // Debug.Log(_bulletPool.GetCountInactive());
        // return Time.time >= timeBetFire + lastFireTime && _IbulletPool.CountInactive > 0;
        return Time.time >= timeBetFire + lastFireTime;
    }

    // Start is called before the first frame update
    // async void Start()
    // {
    //     Debug.Log("Start[Gun] start");
    //     bool isBulletReady = await _bullet.Ready();
    //     Debug.Log($"isBulletReady: {isBulletReady}");
    //     _bulletPool = _bullet.IBulletPool;
    //     Debug.Log($"_bulletPool: {_bulletPool}");
    //     Debug.Log("Start[Gun] end");
    //     IsInitialized = true;
    // }

    // Update is called once per frame
    void Update()
    {
        // if (IsInitialized)
        // {
            if (Performable(timeBetFire))
            {
                Fire();
            }
        // }
    }
}
