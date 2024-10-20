using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField]
    GameData gameData;

    [SerializeField]
    SpawnPointData spawnPointData;

    [SerializeField]
    WeaponData gunData;

    [SerializeField]
    ProjectileData bulletData;

    [SerializeField]
    PlayerData playerData;
    [SerializeField]
    WaveManagerData waveManagerData;
    [SerializeField]
    Square sqaurePrefab;
    [SerializeField]
    Rhombus rhombusPrefab;
    [SerializeField]
    Bullet bulletPrefab;

    [SerializeField]
    Player playerPrefab;

    protected override void Configure(IContainerBuilder builder)
    {
        // register data
        RegisterData(builder);

        builder.RegisterComponentInNewPrefab(playerPrefab, Lifetime.Scoped);
        builder.RegisterComponent(bulletPrefab);
        builder.RegisterComponentInHierarchy<GameManager>().AsSelf();
        builder.RegisterComponentOnNewGameObject<BulletPool>(Lifetime.Scoped, "bulletPool");
        builder.RegisterComponentOnNewGameObject<Gun>(Lifetime.Scoped, "gun").As<IWeapon>();
    }

    private void RegisterData(IContainerBuilder builder)
    {
        builder.RegisterInstance(gameData).AsSelf();
        builder.RegisterInstance(spawnPointData).AsSelf();
        builder.RegisterInstance(gunData).AsSelf();
        builder.RegisterInstance(bulletData).AsSelf();
        builder.RegisterInstance(playerData).AsSelf();
        builder.RegisterInstance(waveManagerData).AsSelf();
    }
}
