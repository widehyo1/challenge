using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class VariableStore
{
    // inputs
    public static readonly string HORIZONTAL = "Horizontal";
    public static readonly string VERTICAL = "Vertical";

    // logger
    public static readonly string GAME_LOGIC = "GameLogic";
    public static readonly string SYSTEM = "System";
    public static readonly string LOG_DIR = "Log";
    public static readonly string LOG_FILE = "log.html";

    public static string LOG_PATH;

    // addresses
    public static readonly string enermyAddress = "Enermy";
    public static List<string> prefabAddresses = new() {
        "Enermy",
        "Player"
    };

    //
    public static Dictionary<string, GameObject> prefabInventory;
    [SerializeField] public static List<WaveInfo> waveInfos;
    [SerializeField] public static List<Vector3> spawnPositionOffset;
    [SerializeField] public static int defaultCapacity = 10;
    [SerializeField] public static int maxSize = 100;

    [Range(0.0f, 10.0f)] public static float enermySpeed = 3f;
    [Range(0.0f, 10.0f)] public static float bulletSpeed = 5f;

    public static GameManager gameManager;

}