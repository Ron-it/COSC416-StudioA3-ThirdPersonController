using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    [SerializeField]
    private int score = 0;

    [Header("UI Elements")]
    [SerializeField]
    private TextMeshProUGUI scoreText; // Reference to the TMP text object

    private void Awake()
    {
        // Implement a basic Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            // Optional: Keep this GameManager across scenes
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // Initialize the score display
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score); // Log the updated score
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score; // Update the TMP text
        }
        else
        {
            Debug.LogWarning("ScoreText is not assigned in the GameManager!");
        }
    }
}
