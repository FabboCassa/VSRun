using UnityEngine;

public class PowerUpManager : ItemManager
{
    public enum PowerUpType
    {
        Invincibility,
        HpUp
    }

    public PowerUpType powerUpType; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            ApplyEffect();
            Destroy(gameObject);
        }
    }

    private void ApplyEffect()
    {
        switch (powerUpType)
        {
            case PowerUpType.Invincibility:
                Debug.Log("Player picked up Invincibility PowerUp!");
                break;

            case PowerUpType.HpUp:
                Debug.Log("Player picked up HpUp PowerUp!");
                break;
        }
    }
}
