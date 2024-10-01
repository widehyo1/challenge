using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILogType
{
    string GetLogType();
}
public class LogType : ILogType
{
    private readonly string LogTypeName;
    public string GetLogType()
    {
        return LogTypeName;
    }

    public LogType(string logTypeName)
    {
        LogTypeName = logTypeName;
    }
}
