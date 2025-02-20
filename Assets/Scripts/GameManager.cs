using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int score, time;
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
        if(data != null) highestScore = data.GetHighScore();
        score = 0;
        time = 0;
        scoreText.text = "Score: " + score;
        StartCoroutine(IncreaseTime());
    }

    private IEnumerator IncreaseTime()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(1);
            AddTime(1); 
        }
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }

    private void AddTime(int points)
    {
        time += points;
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

    public int GetTime()
    {
        return time;
    }
}
