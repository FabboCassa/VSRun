using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public TextMeshProUGUI highestScoreText;
    private int highestScore;

    private void Start()
    {
        SaveSystem.initializeData();
        Data data = SaveSystem.getSavedData();
        if (data == null)
        {
            Debug.Log("No save data found");
            highestScore = 0;
        } else {
            highestScore = data.GetHighScore();
        }
        highestScoreText.text = "Highest Score: " + highestScore;
    }

    public void StartNewGame()
    {
        //load the main game scene
        SceneManager.LoadScene("MainGame");
    }
}
