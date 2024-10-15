using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField]
    GameData gameData;

    [SerializeField]
    SpawnerData spawnerData;

    [SerializeField]
    WeaponData gunData;

    [SerializeField]
    ProjectileData bulletData;

    [SerializeField]
    PlayerData playerData;

    protected override void Configure(IContainerBuilder builder)
    {
        // register data
        RegisterData(builder);

        builder.RegisterComponentInHierarchy<PrefabLoader>().AsSelf();
        builder.RegisterComponentInHierarchy<GameManager>().AsSelf();
        builder.RegisterComponentOnNewGameObject<BulletPool>(Lifetime.Scoped, "bulletPool");
        builder.RegisterComponentOnNewGameObject<Gun>(Lifetime.Scoped, "gun").As<IWeapon>();
        // builder.Register<PlayerMove>(Lifetime.Scoped);
        // builder.RegisterComponentInHierarchy<BulletPool>().AsSelf();
        // builder.RegisterComponentInHierarchy<Bullet>().AsSelf();
        // builder.Register<IProjectile, Bullet>(Lifetime.Scoped);
        // builder.Register<Bullet>(Lifetime.Scoped);
        // builder.RegisterComponentInHierarchy<Gun>().AsSelf();
        // builder.RegisterComponentOnNewGameObject<Player>(Lifetime.Scoped, "player");
    }

    private void RegisterData(IContainerBuilder builder)
    {
        builder.RegisterInstance(gameData).AsSelf();
        builder.RegisterInstance(spawnerData).AsSelf();
        builder.RegisterInstance(gunData).AsSelf();
        builder.RegisterInstance(bulletData).AsSelf();
        builder.RegisterInstance(playerData).AsSelf();
    }
}
