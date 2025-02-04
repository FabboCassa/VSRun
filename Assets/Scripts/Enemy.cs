using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public float speed;
    private bool hasScored = false; // Per evitare punti duplicati
    private GameManager gameManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameManager.Instance;
        int time = gameManager.GetTime();

        if (time < (int)PhaseTime.FirstPhase)
        {
            speed = 10;
        }
        else if (time < (int)PhaseTime.SecondPhase)
        {
            speed = 15;
        }
        else if (time < (int)PhaseTime.ThirdPhase)
        {
            speed = 20;
        }
        else
        {
            speed = 30;
        }


    }

    void Update()
    {
        if (gameManager.IsGameOver()) return;
        transform.position += Vector3.back * speed * Time.deltaTime;

        if (!hasScored && transform.position.z <= 0)
        {
            hasScored = true;
            gameManager.AddScore(1);
        }

        if (transform.position.z <= -5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("Player hit!");
            gameManager.StopSpawner();
            gameManager.GameOver();
        }
    }
}
