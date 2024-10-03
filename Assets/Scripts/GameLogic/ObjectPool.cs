using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Assertions;
using UnityEngine.Pool;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ObjectPool : DebuggableMonoBehaviour
{

    [Tooltip("address for enermy")]
    [SerializeField] public string enermyAddress = "Enermy";

    [Tooltip("address for main character")]
    [SerializeField] public string mainCharacterAddress = "MainCharacter";
    [SerializeField] private AsyncOperationHandle<GameObject> enermyHandle;
    [SerializeField] private AsyncOperationHandle<GameObject> mainCharacterHandle;

    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 10;
    [SerializeField] private int maxSize = 10;

    [SerializeField] public bool IsInitialized { get; private set; }

    [SerializeField] private GameObject enermyPrefab;
    [SerializeField] private GameObject mainCharacterPrefab;

    private readonly LogType logType = new("GameLogic");

    private IObjectPool<Enermy> enermyPool;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override async void Start()
    {
        base.Start();
        enermyPool = new ObjectPool<Enermy>(CreateEnermy, OnGetFromPool, 
            OnReleaseToPool, OnDestroyPooledObject,
            collectionCheck, defaultCapacity, maxSize);
        Log($"object pool started with enermyPool: {enermyPool}", logType);
        enermyHandle = Addressables.LoadAssetAsync<GameObject>(enermyAddress);
        enermyPrefab = await enermyHandle;
        mainCharacterHandle = Addressables.LoadAssetAsync<GameObject>(mainCharacterAddress);
        mainCharacterPrefab = await mainCharacterHandle;
        Instantiate(mainCharacterPrefab, new(0, 0, 0), Quaternion.identity);

        IsInitialized = true;
    }

    public IObjectPool<Enermy> GetEnermyPool()
    {
        return enermyPool;
    }

    // invoked when creating an item to populate the enermy pool
    private Enermy CreateEnermy()
    {
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

    private void OnDestroy()
    {
        Addressables.Release(enermyHandle);
        Addressables.Release(mainCharacterHandle);
    }


}
