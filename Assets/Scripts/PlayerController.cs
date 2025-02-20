using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private EnumPosition currentPosition = EnumPosition.Center; // Partenza al centro

    private float moveDistance = 3f; // Distanza di spostamento

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float minSwipeDistance = 50f; // Distanza minima per considerare uno swipe
    private int life = 3;

    private SpawnManager spawnManager;
    void Start()
    {
        spawnManager = FindFirstObjectByType<SpawnManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    break;

                case TouchPhase.Ended:
                    endTouchPosition = touch.position;
                    DetectSwipe();
                    break;
            }
        }

        if (transform.position.z<25) {
            transform.position += Vector3.forward * 0.0005f;
        }
    }

    void DetectSwipe()
    {
        float swipeDistanceX = endTouchPosition.x - startTouchPosition.x;

        if (Mathf.Abs(swipeDistanceX) > minSwipeDistance)
        {
            if (swipeDistanceX > 0)
            {
                MoveRight(); // Swipe destro
            }
            else
            {
                MoveLeft(); // Swipe sinistro
            }
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

    public int getLife()
    {
        return life;
    }
    public void Hit() {
        life--;
        Debug.Log("Life: " + life);
        if(transform.position.z>10) {
            transform.position += Vector3.back * 3;
        } else if (transform.position.z>=1) {
            transform.position += Vector3.back * 1;
        } 
    }
}
