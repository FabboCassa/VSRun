using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private EnumPosition currentPosition = EnumPosition.Center; // Partenza al centro

    private float moveDistance = 5f; // Distanza di spostamento

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }
    }

    void MoveLeft()
    {
        if (currentPosition == EnumPosition.Center)
        {
            transform.position += Vector3.left * moveDistance;
            currentPosition = EnumPosition.Left;
        }
        else if (currentPosition == EnumPosition.Right)
        {
            transform.position += Vector3.left * moveDistance;
            currentPosition = EnumPosition.Center;
        }
    }

    void MoveRight()
    {
        if (currentPosition == EnumPosition.Center)
        {
            transform.position += Vector3.right * moveDistance;
            currentPosition = EnumPosition.Right;
        }
        else if (currentPosition == EnumPosition.Left)
        {
            transform.position += Vector3.right * moveDistance;
            currentPosition = EnumPosition.Center;
        }
    }
}
