using System.IO;
using UnityEngine;

public interface IDebuggerable
{

}
public class DebuggableMonoBehaviour : MonoBehaviour, IDebuggerable
{
    private Logger logger;
    private bool isDebugBuild;
    protected virtual void Awake()
    { 
        isDebugBuild = Debug.isDebugBuild;
        if (isDebugBuild)
        {
            LogType logType = new("System");
            Log($"{GetType().Name}.Awake", logType);
        }
        else
        {
            // Debug.Log($"{GetType().Name}.Awake");
        }
    }

    protected virtual void Start()
    { 
        if (isDebugBuild)
        {
            LogType logType = new("System");
            Log($"{GetType().Name}.Start", logType);
        }
        else
        {
            // Debug.Log($"{GetType().Name}.Start");
        }
    }


    private Logger GetLogger()
    {
        Logger logger = FindAnyObjectByType<Logger>();
        if (logger == null)
        {
            Debug.Log("logger not found");
            logger = SpawnLogger();
        }
        return logger;
    }

    private Logger SpawnLogger()
    {
        Debug.Log("spawn logger");
        Logger.LogFilePath = Path.Combine(Application.persistentDataPath, "Log", "log.html");
        Debug.Log(Logger.LogFilePath);
        Logger logger = gameObject.AddComponent<Logger>();
        return logger;
    }

    protected virtual void Log(string message, ILogType logType)
    {
        if (!isDebugBuild) return;
        logger = GetLogger();
        logger.Log(message, logType);
    }
}
