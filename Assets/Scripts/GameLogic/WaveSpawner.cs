using System.Collections.Generic;
using System.Text;
using UnityEngine;
using VContainer;

public class WaveSpawner : MonoBehaviour
{
    private SpawnPointData _spawnPointData;
    private WaveManagerData _waveManagerData;
    [Inject]
    public void Construct(SpawnPointData spawnPointData, WaveManagerData waveManagerData)
    {
        Debug.Log("Construction[WaveSpawner] start");
        _spawnPointData = spawnPointData;
        _waveManagerData = waveManagerData;
        Debug.Log("Construction[WaveSpawner] end");
    }
}