using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    private int score = 0;
    private SpawnManager spawnManager;
    private bool isGameOver = false;

    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        spawnManager = FindFirstObjectByType<SpawnManager>();
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
        // Controlla se il punteggio corrente è maggiore dell'HighestScore
        int highestScore = PlayerPrefs.GetInt("HighestScore", 0);
        if (score > highestScore)
        {
            PlayerPrefs.SetInt("HighestScore", score);
            PlayerPrefs.Save();
        }

        // Torna al menu principale
        SceneManager.LoadScene("MainMenu");
    }
}
