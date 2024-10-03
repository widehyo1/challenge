using UnityEngine;

public class GlobalGameManager : MonoBehaviour
{
    public GameManager gameManager;
    void Awake()
    {
        GetComponent<DebuggableMonoBehaviour>();
        gameManager = gameObject.AddComponent<GameManager>();
    }

}