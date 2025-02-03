using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public float speed;
    private bool hasScored = false; // Per evitare punti duplicati

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (GameManager.Instance.IsGameOver()) return;
        transform.position += Vector3.back * speed * Time.deltaTime;

        // Controllo se il nemico ha superato Z = 0
        if (!hasScored && transform.position.z <= 0)
        {
            hasScored = true; // Evita punti doppi
            GameManager.Instance.AddScore(1);
        }

        // Elimina l'oggetto quando supera Z = -5
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
            GameManager.Instance.StopSpawner();
            GameManager.Instance.GameOver();
        }
    }
}
