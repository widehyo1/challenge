using UnityEngine;
using UnityEngine.Pool;

public interface IProjectilePool
{
    public IProjectile Get();
    public IObjectPool<IProjectile> GetProjectilePool();
    public int GetCountInactive();
}
