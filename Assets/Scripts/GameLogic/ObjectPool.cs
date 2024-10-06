using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Pool;

public class ObjectPool : DebuggableMonoBehaviour
{
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] public bool IsInitialized { get; private set; }
    private readonly LogType logType = new(VariableStore.GAME_LOGIC);
    private IObjectPool<Enermy> enermyPool;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        if (VariableStore.prefabInventory == null)
        {
            Log($"gameManager is not set", logType);
            return;
        }
        enermyPool = new ObjectPool<Enermy>(CreateEnermy, OnGetFromPool, 
            OnReleaseToPool, OnDestroyPooledObject,
            collectionCheck, VariableStore.defaultCapacity, VariableStore.maxSize);
        Log($"object pool started with enermyPool: {enermyPool}", logType);
        IsInitialized = true;
    }

    public void InstantiateMainCharacter()
    {
        var pi = VariableStore.prefabInventory;
        pi.TryGetValue(VariableStore.mainCharacterAddress, out var mainCharacterPrefab);
        Instantiate(mainCharacterPrefab, new(0, 0, 0), Quaternion.identity);
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
