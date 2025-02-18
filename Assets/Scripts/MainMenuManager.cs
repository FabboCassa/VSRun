using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Netcode;

public class MainMenuManager : NetworkBehaviour 
{
    [SerializeField] private TextMeshProUGUI highestScoreText;
    [SerializeField] private Button newGameButton, onlineButton, hostButton, joinButton, backButton;
    private NetworkManager networkManager;

    private void Start()
    {
        SaveSystem.initializeData();
        Data data = SaveSystem.getSavedData();
        networkManager = NetworkManager.Singleton;
        MainMenu();
        if (data == null)
        {
            Debug.Log("No save data found");
        }
    }

    public void StartNewGame()
    {
        //load the main game scene
        SceneManager.LoadScene("MainGame");
    }

    private void OnlineScene() {
        newGameButton.gameObject.SetActive(false);
        onlineButton.gameObject.SetActive(false);
        hostButton.gameObject.SetActive(true);
        joinButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
    }

    private void MainMenu()
    {
        newGameButton.gameObject.SetActive(true);
        onlineButton.gameObject.SetActive(true);
        hostButton.gameObject.SetActive(false);
        joinButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(false);
    }

    public void BackButton() {
        MainMenu();
    }

    public void OnlineButton() {
        OnlineScene();
    }

    public void HostButton() {
        networkManager.StartHost();
        networkManager.SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }
    public void ClientButton() {
        networkManager.StartClient();
        networkManager.SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
    }
}
