using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    [SerializeField]
    private int score = 0;

    private void Awake()
    {
        // Implement a basic Singleton pattern.
        if (Instance == null)
        {
            Instance = this;
            // Optional: Keep this GameManager across scenes.
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
        // Here you can update UI text or perform other actions.
    }
}
