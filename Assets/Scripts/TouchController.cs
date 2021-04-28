using System;
using UnityEngine;

public class TouchController : MonoBehaviour
{

    public static TouchController Instance { get; private set; }

    [Range(0.001f, 0.015f)]
    public float magnitude = 0.001f;

    private float leftBorder = -0.16f;
    private float rightBorder = 0.16f;

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

    private void Update()
    {

        if (Input.GetMouseButton(0))
            OnTouchBegan();

        if (Input.GetMouseButtonUp(0))
            OnTouchEnded();

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            OnTouchBegan();

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            OnTouchEnded();

    }

    private void OnTouchBegan()
    {
        if (SwipeController.Instance.direction == Vector3.left && gameObject.transform.position.x <= leftBorder
        || SwipeController.Instance.direction == Vector3.right && gameObject.transform.position.x >= rightBorder)
        {
            return;
        }
        gameObject.transform.position = transform.position + SwipeController.Instance.direction * magnitude;
    }

    private void OnTouchEnded()
    {
        SwipeController.Instance.direction = Vector3.zero;
    }

}
