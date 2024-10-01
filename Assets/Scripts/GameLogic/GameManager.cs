using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(ObjectPool))]
public class GameManager : DebuggableMonoBehaviour
{

    private ObjectPool objectPool;

    [SerializeField] private int enermyCount = 10;

    [Range(0f, 3f)]
    [SerializeField] private float spacing = 1.5f;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        objectPool = gameObject.AddComponent<ObjectPool>();
        ILogType logtype = new LogType("GameLogic");
        Log("objectPool is added to gameObject's component", logtype);
        StartCoroutine(WaitForObjectPoolInitialization());
    }


    private IEnumerator WaitForObjectPoolInitialization()
    {
        yield return new WaitUntil(() => objectPool.IsInitialized);

        GenerateEnermy();
    }

    public void GenerateEnermy()
    {
        IObjectPool<Enermy> enermyPool = objectPool.GetEnermyPool();
        for (int i = 0; i < enermyCount; i++)
        {
            Enermy enermy = enermyPool.Get();
            enermy.transform.SetPositionAndRotation(new Vector3(i * spacing - (enermyCount / 2 * spacing), -6, 0), Quaternion.identity);
        }

    }


}
