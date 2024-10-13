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

        builder.RegisterComponentInHierarchy<PlayerMove>().AsSelf();
        builder.RegisterComponentInHierarchy<PrefabLoader>().AsSelf();
        builder.RegisterComponentInHierarchy<BulletPool>().AsSelf();
        builder.RegisterComponentInHierarchy<Bullet>().AsSelf();
        // builder.Register<IProjectile, Bullet>(Lifetime.Scoped);
        builder.RegisterComponentInHierarchy<Gun>().AsSelf();
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
