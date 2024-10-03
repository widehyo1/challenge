using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(ObjectPool))]
public class GameManager : DebuggableMonoBehaviour
{
    private ObjectPool objectPool;
    private readonly LogType logtype = new("GameLogic");
    [SerializeField] private int enermyCount = 10;

    [Range(0f, 3f)]
    [SerializeField] private float spacing = 1.5f;
    protected override void Awake()
    {
        base.Awake();
        objectPool = GetComponent<ObjectPool>();
        Log("gameManager[Awake]: get objectPool component", logtype);
    }

    protected override void Start()
    {
        base.Start();
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
