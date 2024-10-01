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