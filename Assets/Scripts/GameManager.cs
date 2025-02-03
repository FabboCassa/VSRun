using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int score;
    private SpawnManager spawnManager;
    private bool isGameOver = false;

    public TextMeshProUGUI scoreText;
    private int highestScore;
    private Data data;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        data = SaveSystem.getSavedData();
        spawnManager = FindFirstObjectByType<SpawnManager>();
        highestScore = data.GetHighScore();
        score = 0;
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
        scoreText.text = "Score: " + score;
    }

    public void StopSpawner()
    {
        Debug.Log("Game Over! Spawning stopped.");
        spawnManager.StopSpawning();
        isGameOver = true;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }

    public void GameOver()
    {

        if (score > highestScore)
        {
            Debug.Log("New high score!");
            highestScore = score;
            data.SetHighScore(highestScore);
            SaveSystem.SaveData(data);
        }

        // Back to main menu
        SceneManager.LoadScene("MainMenu");
    }

    public int GetHighScore()
    {
        return score;
    }
}
