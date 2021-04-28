using System;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    public static SwipeController Instance { get; private set; }

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    public Vector3 direction;


    [SerializeField]
    private float minDistanceForSwipe = 5f;

    private void Awake()
    {
        #region Singleton

        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(Instance.gameObject);
        }

        #endregion //Singleton
    }
    public void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }
        }
    }

    private void DetectSwipe()
    {
        if (SwipeDistanceCheckMet())
        {

            direction = fingerDownPosition.x - fingerUpPosition.x > 0 ? Vector3.right : Vector3.left;

            fingerUpPosition = fingerDownPosition;
        }
    }

    private bool SwipeDistanceCheckMet()
    {
        return HorizontalMovementDistance() > minDistanceForSwipe;
    }

    private float HorizontalMovementDistance()
    {
        return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
    }

}
