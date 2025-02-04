using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private EnumPosition currentPosition = EnumPosition.Center; // Partenza al centro

    private float moveDistance = 3f; // Distanza di spostamento

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float minSwipeDistance = 50f; // Distanza minima per considerare uno swipe

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
}
