using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer;

public class PrefabLoader : DebuggableMonoBehaviour
{
    private readonly LogType logType = new("GameLogic");

    // prefabLoader는 하나의 lifetime scope에 단 하나 존재하여
    // prefabInventory를 제공해주기 위해 설계한 클래스.
    // prefab을 필요로 하는 클래스는 모두 VContainer에서 이 instance를
    // 주입받아 prefabInventory에서 원하는 프리팹을 꺼내 쓰기 위해 설계함
    // 
    // 문제:
    // prefab을 필요로 하는 class에서 VContainer를 이용하여 DI로
    // prefabLoader를 참조하는 것은 좋으나 실제 prefabLoader의
    // prefabInventory가 이용 가능해지는 시점은
    // MonoBehavior 인 prefabLoader가 Start()에서 await를 하고 난 이후의 시점
    // 그러나 prefab을 필요로 하는 class에서 DI로 주입받은 instance는
    // async Start()가 await되기 이전 시점의 instance로
    // prefabInventory가 비어있음
    // 임시방편으로 static을 이용하여 처리했으나 로직 수정이 필요해 보임
    // Unity PlayerLoop lifecycle와 VContainer의 결합에 대한 이해도 필요
    // 
    // prefabLoader가 MonoBehaviour인 이유:
    // OnDestroy 시점에 prefabHandle을 자동적으로 Release하기 위해
    public static bool IsInitialized { get; private set; }
    public AsyncOperationHandle<IList<GameObject>> prefabHandle;

    public static Dictionary<string, GameObject> prefabInventory;

    private GameData _gameData;

    [Inject]
    public void Construct(GameData gameData)
    {
        Debug.Log($"Construction[PrefabLoader] start");
        Debug.Log($"gameData: {gameData}");
        _gameData = gameData;
        prefabInventory = new();
        Debug.Log($"Construction[PrefabLoader] end");
    }

    public void ReleaseHandle()
    {
        Addressables.Release(prefabHandle);
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override async void Start()
    {
        base.Start();
        List<string> addresses = _gameData.prefabAddresses;
        prefabHandle = Addressables.LoadAssetsAsync<GameObject>(
            addresses, // Either a single key or a List of keys 
            addressable =>
            {
                // Called for every loaded asset
                Log($"prefab {addressable.name} loaded!", logType);
                prefabInventory.Add(addressable.name, addressable);
            }, Addressables.MergeMode.Union, // How to combine multiple labels 
            false); // Whether to fail if any asset fails to load

        // Wait for the operation to finish in the background
        await prefabHandle.Task;
        Log("prefab loads end", new LogType("GameLogic"));
        IsInitialized = true;
    }

    public void OnDestroy()
    {
        Addressables.Release(prefabHandle);
        Log("prefab release end", new LogType("GameLogic"));
    }

}
