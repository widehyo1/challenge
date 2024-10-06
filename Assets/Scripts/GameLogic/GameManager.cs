using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

public class GameManager : DebuggableMonoBehaviour
{
    private ObjectPool objectPool;
    private WaveInfoManager waveInfoManager;
    private PrefabLoader prefabLoader;
    private readonly LogType logtype = new(VariableStore.GAME_LOGIC);
    [SerializeField] private int enermyCount = 10;
    [Range(0f, 3f)]
    [SerializeField] private float spacing = 1.5f;
    protected override void Awake()
    {
        base.Awake();
        prefabLoader = GetComponent<PrefabLoader>();
        waveInfoManager = GetComponent<WaveInfoManager>();
        Log("gameManager[Awake]: get objectPool component", logtype);
    }

    protected override async void Start()
    {
        base.Start();
        waveInfoManager.Load();
        Log("gameManager[Start]: waveInfoManager.Load() finished", logtype);
        await UniTask.WaitUntil(() => prefabLoader.IsInitialized);
        Log("gameManager[Start]: prefabLoader.IsInitialized", logtype);
        objectPool = gameObject.AddComponent<ObjectPool>();
        await UniTask.WaitUntil(() => objectPool.IsInitialized);
        Log("gameManager[Start]: objectPool.IsInitialized", logtype);
        VariableStore.gameManager = this;
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

    public void InstantiateMainCharacter()
    {
        objectPool.InstantiateMainCharacter();
    }


}
