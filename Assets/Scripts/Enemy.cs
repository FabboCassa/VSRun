using UnityEngine;

public class Enemy : ItemManager
{
    
    
    private bool hasScored = false; // Per evitare punti duplicati
    
    

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        if (gameManager.IsGameOver()) return;
        
        base.Update();

        if (!hasScored && transform.position.z <= 0)
        {
            hasScored = true;
            gameManager.AddScore(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("Player hit!");
            //gameManager.StopSpawner();
            //gameManager.GameOver();
        }
    }
}
