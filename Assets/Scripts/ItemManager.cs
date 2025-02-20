using UnityEngine;
using Unity.Netcode;

public class ItemManager : NetworkBehaviour
{
    protected GameManager gameManager;
    protected float speed;
    protected GameObject player;
    protected enum SpeedPhase
    {
        FirstPhaseSpeed = 10,
        SecondPhaseSpeed = 15,
        ThirdPhaseSpeed = 20,
        FourthPhaseSpeed = 30
    }
    protected void Start()
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

    // Update is called once per frame
    protected void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;
        if (transform.position.z <= -5)
        {
            Destroy(gameObject);
        }
    }
}
