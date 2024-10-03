using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;
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
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 100;

    [SerializeField] public bool IsInitialized { get; private set; }

    [SerializeField] private GameObject enermyPrefab;
    [SerializeField] private GameObject mainCharacterPrefab;

    private readonly LogType logType = new("GameLogic");

    private IObjectPool<Enermy> enermyPool;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        enermyPool = new ObjectPool<Enermy>(CreateEnermy, OnGetFromPool, 
            OnReleaseToPool, OnDestroyPooledObject,
            collectionCheck, defaultCapacity, maxSize);
        Log($"object pool started with enermyPool: {enermyPool}", logType);
        enermyHandle = Addressables.LoadAssetAsync<GameObject>(enermyAddress);
        enermyHandle.Completed += EnermyHandleCompleted;
        mainCharacterHandle = Addressables.LoadAssetAsync<GameObject>(mainCharacterAddress);
        mainCharacterHandle.Completed += MainCharacterHandleCompleted;

        IsInitialized = true;
    }

    private void MainCharacterHandleCompleted(AsyncOperationHandle<GameObject> operation)
    {
        if (operation.Status == AsyncOperationStatus.Succeeded)
        {
            mainCharacterPrefab = operation.Result;
            Instantiate(mainCharacterPrefab, new(0, 0, 0), Quaternion.identity);
        }
        else
        {
            Log($"Asser for {mainCharacterAddress} failed to load.", logType);
        }
    }

    private void EnermyHandleCompleted(AsyncOperationHandle<GameObject> operation)
    {
        if (operation.Status == AsyncOperationStatus.Succeeded)
        {
            enermyPrefab = operation.Result;
        }
        else
        {
            Log($"Asser for {enermyAddress} failed to load.", logType);
        }
    }

    public IObjectPool<Enermy> GetEnermyPool()
    {
        return enermyPool;
    }

    // invoked when creating an item to populate the enermy pool
    private Enermy CreateEnermy()
    {
        if (enermyPrefab == null)
        {
            Debug.Log("enermyPrefab is null");
            return null;
        }
        Debug.Log($"enermyPrefab: {enermyPrefab}");
        GameObject enermyInstance = Instantiate(enermyPrefab, new(0, 0, 0), Quaternion.identity);
        if (enermyInstance == null)
        {
            Debug.Log("enermyInstance is null");
            return null;
        }
        if (enermyInstance.TryGetComponent<Enermy>(out var enermy))
        {
            enermy.EnermyPool = enermyPool;
        }
        else
        {
            Debug.Log("enermyInstance does not has component [Enermy]");
            return null;
        }
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
