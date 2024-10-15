using UnityEngine;
using UnityEngine.Pool;

public interface IProjectilePool
{
    public IProjectile Get();
    public void SetPrefab(GameObject prefab);
    public void SetPlayer(Player player);
    public IObjectPool<IProjectile> GetProjectilePool();
}
