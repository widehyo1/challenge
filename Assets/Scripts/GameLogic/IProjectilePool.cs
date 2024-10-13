using UnityEngine;
using UnityEngine.Pool;

public interface IProjectilePool
{
    public IObjectPool<IProjectile> GetProjectilePool();
}
