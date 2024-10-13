using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class WaveInfoManager : DebuggableMonoBehaviour
{
    [SerializeField] public static List<WaveInfo> waveInfos = new();
    [SerializeField] public static List<Vector3> spawnPositionOffset = new();

    private void AddWaveInfo(int waveNumber, List<Composition> compositions)
    {
        waveInfos.Add(new WaveInfo(waveNumber, compositions));
    }
    private void LoadWaveInfos()
    {
        AddWaveInfo(1, new()
        {
            new("Enermy", 10, 10)
        });
        AddWaveInfo(2, new()
        {
            new("Enermy", 20, 40)
        });
        VariableStore.waveInfos = waveInfos;
    }
    private void SetSpawnPositionOffset()
    {
        float spacing = 1.5f;
        for (int i = 0; i < 10; i++)
        {
            spawnPositionOffset.Add(new (i * spacing - (5 * spacing), -6, 0));
        }
        VariableStore.spawnPositionOffset = spawnPositionOffset;
    }

    public void Load()
    {
        LoadWaveInfos();
        SetSpawnPositionOffset();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }
}

public class Composition
{
    public string enermyAddress;
    public int enermyDefaultCapacity;
    public int enermyMaxSize;
    public Composition(string enermyAddress, int enermyDefaultCapacity, int enermyMaxSize)
    {
        this.enermyAddress = enermyAddress;
        this.enermyDefaultCapacity = enermyDefaultCapacity;
        this.enermyMaxSize = enermyMaxSize;
    }

    public override string ToString()
    {
        return $"({enermyAddress}, {enermyDefaultCapacity}, {enermyMaxSize})";
    }
}
public class WaveInfo
{
    public int waveNumber;
    public List<Composition> compositions;

    public WaveInfo(int waveNumber, List<Composition> compositions)
    {
        this.waveNumber = waveNumber;
        this.compositions = compositions;
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append($"waveNumber: {waveNumber}, compositions:");
        foreach (Composition composition in compositions)
        {
            sb.Append($"{composition} ");
        }
        return sb.ToString();
    }
}