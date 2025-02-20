using UnityEngine;
using Unity.Netcode;

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
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit!");

            var playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.Hit();

                if (playerController.getLife() == 0)
                {
                    // Qui possiamo chiamare un ServerRpc per distruggere il nemico lato server
                    RequestDestroyServerRpc();
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }

     [ServerRpc]
    private void RequestDestroyServerRpc()
    {
        if (IsServer) // Solo il server può distruggere oggetti in rete
        {
            Destroy(gameObject);
        }
    }
}
