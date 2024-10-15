using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer;

public class PrefabLoader : MonoBehaviour
{
    private bool isInitialized;
    public bool IsInitialized {
        get => isInitialized;
        private set => isInitialized = value;
    }
    private Dictionary<string, GameObject> prefabInventory;
    public Dictionary<string, GameObject> PrefabInventory {
        get => prefabInventory;
        private set => prefabInventory = value;
    }
    private AsyncOperationHandle<IList<GameObject>> prefabHandle;
    private GameData _gameData;

    [Inject]
    public void Construct(GameData gameData)
    {
        Debug.Log($"Construction[PrefabLoader] start");
        Debug.Log($"gameData: {gameData}");
        _gameData = gameData;
        Debug.Log($"Construction[PrefabLoader] end");
    }

    public void ReleaseHandle()
    {
        Addressables.Release(prefabHandle);
    }

    async void Start()
    {
        prefabInventory = new();
        List<string> addresses = _gameData.prefabAddresses;
        prefabHandle = Addressables.LoadAssetsAsync<GameObject>(
            addresses, // Either a single key or a List of keys 
            addressable =>
            {
                // Called for every loaded asset
                Debug.Log($"prefab {addressable.name} loaded!");
                prefabInventory.Add(addressable.name, addressable);
            }, Addressables.MergeMode.Union, // How to combine multiple labels 
            false); // Whether to fail if any asset fails to load

        // Wait for the operation to finish in the background
        await prefabHandle.Task;
        Debug.Log("prefab loads end");
        IsInitialized = true;
    }

    public void OnDestroy()
    {
        Addressables.Release(prefabHandle);
        Debug.Log("prefab release end");
    }

}
