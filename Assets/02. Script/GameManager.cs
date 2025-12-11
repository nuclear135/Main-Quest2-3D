using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("점수 설정")]
    [SerializeField] private int targetScore = 5;
    [SerializeField] private Text scoreText;
    private int currentScore = 0;

    [Header("게임 상태")]
    [SerializeField] private GameObject ReStartPanel;
    private GameState state = GameState.Ready;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateScoreUI();
        state = GameState.Playing;
        if (ReStartPanel != null)
            ReStartPanel.SetActive(false);
    }

    public void AddScore(int amount)
    {
        if (state != GameState.Playing) return;

        currentScore += amount;
        UpdateScoreUI();

        if (currentScore >= targetScore)
        {
            SetState(GameState.ReStart);
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore}";
        }
    }

    private void SetState(GameState newState)
    {
        state = newState;

        switch (state)
        {
            case GameState.Ready:
                break;

            case GameState.Playing:
                break;

            case GameState.ReStart:
                Debug.Log("🎉 클리어!");
                Time.timeScale = 0f; // 게임 정지
                if (ReStartPanel != null)
                    ReStartPanel.SetActive(true); // 클리어 UI 띄움
                break;
        }
    }
}
