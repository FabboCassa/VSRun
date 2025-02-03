using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public TextMeshProUGUI highestScoreText; // Riferimento al testo UI per il punteggio più alto

    private void Start()
    {
        // Carica il punteggio più alto salvato, se non esiste usa 0
        int highestScore = PlayerPrefs.GetInt("HighestScore", 0);
        highestScoreText.text = "Highest Score: " + highestScore;
    }

    public void StartNewGame()
    {
        // Resetta il punteggio corrente a 0
        PlayerPrefs.SetInt("CurrentScore", 0);
        PlayerPrefs.Save();
        
        // Carica la scena del gioco
        SceneManager.LoadScene("MainGame");
    }
}
