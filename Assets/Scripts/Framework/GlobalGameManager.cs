using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalGameManager : MonoBehaviour
{
    public GameObject gameoverText;
    public TMP_Text timeText;
    public TMP_Text recordText;
    public Button playButton;

    private float surviveTime;
    private string gameStatus;
    public GameManager gameManager;
    void Awake()
    {
        GetComponent<DebuggableMonoBehaviour>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager")?.GetComponent<GameManager>();
        gameStatus = "Init";
    }

    public void PlayButtonClick()
    {
        playButton.onClick.AddListener(() => GameStart());
    }

    public void GameStart()
    {
        Debug.Log("game start clicked");
        surviveTime = 0f;
        gameStatus = "Play";
        gameManager.GenerateEnermy();
        gameManager.InstantiateMainCharacter();
        playButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameStatus == "Play")
        {
            surviveTime += Time.deltaTime;
            timeText.text = $"Time: {surviveTime}";
        }
        else
        {

        }
    }

    public void EndGame()
    {
        gameStatus = "GameOver";
        gameoverText.SetActive(true);

        float bestTime = PlayerPrefs.GetFloat("BestTime");

        bestTime = Mathf.Max(surviveTime, bestTime);
        PlayerPrefs.SetFloat("BestTime", bestTime);

        recordText.text = $"Best Time: {bestTime}";
    }

}