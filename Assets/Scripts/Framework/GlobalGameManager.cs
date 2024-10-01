using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameManager : MonoBehaviour
{
    public GameManager gameManager;
    void Awake()
    {
        gameObject.AddComponent<DebuggableMonoBehaviour>();
        gameManager = gameObject.AddComponent<GameManager>();
    }

}