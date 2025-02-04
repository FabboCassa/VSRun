using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public float speed;
    private bool hasScored = false; // Per evitare punti duplicati
    private GameManager gameManager;
    private enum SpeedPhase
    {
        FirstPhaseSpeed = 10,
        SecondPhaseSpeed = 15,
        ThirdPhaseSpeed = 20,
        FourthPhaseSpeed = 30
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameManager.Instance;
        int time = gameManager.GetTime();

        if (time < (int)PhaseTime.FirstPhase)
        {
            speed = (float)SpeedPhase.FirstPhaseSpeed;
        }
        else if (time < (int)PhaseTime.SecondPhase)
        {
            speed = (float)SpeedPhase.SecondPhaseSpeed;
        }
        else if (time < (int)PhaseTime.ThirdPhase)
        {
            speed = (float)SpeedPhase.ThirdPhaseSpeed;
        }
        else
        {
            speed = (float)SpeedPhase.FourthPhaseSpeed;
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
