using System;
using System.IO;
using UnityEngine;

public class Logger : MonoBehaviour
{
    static private string logFilePath;
    static private int logCount;

    public static string LogFilePath {
        get { return logFilePath; }
        set { logFilePath = value; }
    }

    void Awake()
    {
        InitializeLogFile();
    }

    private void InitializeLogFile()
    {
        string initialLogFile = @"
<script language='javascript' src='log.js'></script>
<link rel='stylesheet' type='text/css' href='log.css'/>
<div class='Header'>
    <input type='button' value='Error' class='Error Button' onclick='hide_class('Error')'/>
    <input type='button' value='Assert' class='Assert Button' onclick='hide_class('Assert')'/>
    <input type='button' value='Warning' class='Warning Button' onclick='hide_class('Warning')'/>
    <input type='button' value='Log' class='Log Button' onclick='hide_class('Log')'/>
    <input type='button' value='Exception' class='Exception Button' onclick='hide_class('Exception')'/>
    <input type='button' value='GUI' class='GUI Button' onclick='hide_class('GUI')'/>
    <input type='button' value='GUIMessage' class='GUIMessage Button' onclick='hide_class('GUIMessage')'/>
    <input type='button' value='Physics' class='Physics Button' onclick='hide_class('Physics')'/>
    <input type='button' value='Graphics' class='Graphics Button' onclick='hide_class('Graphics')'/>
    <input type='button' value='Terrain' class='Terrain Button' onclick='hide_class('Terrain')'/>
    <input type='button' value='Audio' class='Audio Button' onclick='hide_class('Audio')'/>
    <input type='button' value='Networking' class='Networking Button' onclick='hide_class('Networking')'/>
    <input type='button' value='NetworkServer' class='NetworkServer Button' onclick='hide_class('NetworkServer')'/>
    <input type='button' value='NetworkClient' class='NetworkClient Button' onclick='hide_class('NetworkClient')'/>
    <input type='button' value='GameLogic' class='GameLogic Button' onclick='hide_class('GameLogic')'/>
    <input type='button' value='AI' class='AI Button' onclick='hide_class('AI')'/>
    <input type='button' value='Content' class='Content Button' onclick='hide_class('Content')'/>
    <input type='button' value='System' class='System Button' onclick='hide_class('System')'/>
    <input type='button' value='Input' class='Input Button' onclick='hide_class('Input')'/>
    <input type='button' value='Replay' class='Replay Button' onclick='hide_class('Replay')'/>
    <br/>
</div>";
        string nowString = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        initialLogFile = string.Concat(initialLogFile, $"<h1 class='date'>{nowString}</h1>");
        initialLogFile = string.Concat(initialLogFile, $"<p>Click on buttons to toggle visability. Click on STACK buttons to toggle visibility of stack traces.</p>");
        File.WriteAllText(logFilePath, initialLogFile);
    }

    public string LogToHTML(string message, string logType)
    {
        string pLog = $"<p class='{logType}'><span class='time'>{Time.realtimeSinceStartup}</span><a onclick=\"hide('trace{logCount}')\">STACK</a>{message}</p>";
        string preLog = $"<pre id=\"trace{logCount}\">{Environment.StackTrace}</pre>";
        string htmlLog = string.Concat(pLog, preLog);
        logCount += 1;
        return htmlLog;
    }

    public void Log(string message, ILogType logType)
    {
        Debug.Log(message);
        File.AppendAllText(logFilePath, LogToHTML(message, logType.GetLogType()));
    }

    public void LogHandler(string message, string context, LogType logType)
    {
        // empty
    }

}
