using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PrefabLoader : DebuggableMonoBehaviour
{
    private readonly LogType logType = new("GameLogic");
    public bool IsInitialized { get; private set; }
    public AsyncOperationHandle<IList<GameObject>> prefabHandle;

    public Dictionary<string, GameObject> prefabInventory;

    public void ReleaseHandle()
    {
        Addressables.Release(prefabHandle);
    }

    protected override void Awake()
    {
        base.Awake();
        prefabInventory = new();
    }

    protected override async void Start()
    {
        base.Start();
        List<string> addresses = VariableStore.prefabAddresses;
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
        VariableStore.prefabInventory = prefabInventory;
        IsInitialized = true;
    }

    public void OnDestroy()
    {
        Addressables.Release(prefabHandle);
        Log("prefab release end", new LogType("GameLogic"));
    }

}
