using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] public bool IsInitialized { get; private set; }
    private readonly LogType logType = new(VariableStore.GAME_LOGIC);
    private IObjectPool<Enermy> enermyPool;

    void Awake()
    {
    }

    void Start()
    {
        if (VariableStore.prefabInventory == null)
        {
            Debug.Log($"gameManager is not set");
            return;
        }
        enermyPool = new ObjectPool<Enermy>(CreateEnermy, OnGetFromPool, 
            OnReleaseToPool, OnDestroyPooledObject,
            collectionCheck, VariableStore.defaultCapacity, VariableStore.maxSize);
        Debug.Log($"object pool started with enermyPool: {enermyPool}");
        IsInitialized = true;
    }

    public void InstantiatePlayer()
    {
        var pi = VariableStore.prefabInventory;
        // pi.TryGetValue(VariableStore.PlayerAddress, out var PlayerPrefab);
        // Instantiate(PlayerPrefab, new(0, 0, 0), Quaternion.identity);
    }

    public IObjectPool<Enermy> GetEnermyPool()
    {
        return enermyPool;
    }

    // invoked when creating an item to populate the enermy pool
    private Enermy CreateEnermy()
    {
        var pi = VariableStore.prefabInventory;
        pi.TryGetValue(VariableStore.enermyAddress, out var enermyPrefab);
        Assert.IsNotNull(enermyPrefab, "enermyPrefab is null");
        GameObject enermyInstance = Instantiate(enermyPrefab, new(0, 0, 0), Quaternion.identity);
        Assert.IsNotNull(enermyInstance, "enermyInstance is null");
        Assert.IsTrue(enermyInstance.TryGetComponent<Enermy>(out var enermy), "enermyInstance does not has component [Enermy]");
        enermy.EnermyPool = enermyPool;
        return enermy;
    }

    private void OnReleaseToPool(Enermy pooledEnermy)
    {
        pooledEnermy.gameObject.SetActive(false);
    }

    private void OnGetFromPool(Enermy pooledEnermy)
    {
        pooledEnermy.gameObject.SetActive(true);
    }

    private void OnDestroyPooledObject(Enermy pooledEnermy)
    {
        Destroy(pooledEnermy.gameObject);
    }

}
