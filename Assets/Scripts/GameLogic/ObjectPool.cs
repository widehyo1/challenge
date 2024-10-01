using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : DebuggableMonoBehaviour
{

    [Tooltip("Prefab for enermy")]
    [SerializeField] public GameObject enermyPrefab;

    [SerializeField] private bool collectionCheck = true;
    [SerializeField] private int defaultCapacity = 20;
    [SerializeField] private int maxSize = 100;

    [SerializeField] public bool IsInitialized { get; private set; }

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
        ILogType logType = new LogType("GameLogic");
        Log($"object pool started with enermyPool: {enermyPool}", logType);
        IsInitialized = true;
    }

    public IObjectPool<Enermy> GetEnermyPool()
    {
        return enermyPool;
    }

    // invoked when creating an item to populate the enermy pool
    private Enermy CreateEnermy()
    {
        enermyPrefab = Resources.Load<GameObject>("Prefabs/Enermy");
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

}
