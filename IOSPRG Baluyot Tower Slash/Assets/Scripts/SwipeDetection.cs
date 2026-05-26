using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum SwipeType
{
    UP,
    RIGHT,
    DOWN,
    LEFT,
    NONE
}

public class TouchInput : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    private InputAction _touchPressedAction;
    private InputAction _touchPositionAction;

    private Vector2 _touchStart;
    private Vector2 _touchEnd;
    private Vector2 _distance;

    [SerializeField] private float minDistance = 50f;

    public SwipeType swipeType = SwipeType.NONE;

    public static TouchInput Instance;

    private void Awake()
    {
        Instance = this;

        _touchPressedAction = _playerInput.actions["TouchPress"];
        _touchPositionAction = _playerInput.actions["TouchPosition"];
    }

    private void OnEnable()
    {
        _touchPressedAction.started += OnTouchStarted;
        _touchPressedAction.canceled += OnTouchReleased;
    }


    private void OnDisable()
    {
        _touchPressedAction.started -= OnTouchStarted;
        _touchPressedAction.canceled -= OnTouchReleased;
    }

    private void OnTouchStarted(InputAction.CallbackContext context)
    {
        _touchStart = _touchPositionAction.ReadValue<Vector2>();

    }

    private void OnTouchReleased(InputAction.CallbackContext context)
    {
        _touchEnd = _touchPositionAction.ReadValue<Vector2>();

        DetectSwipe();
    }
    private void DetectSwipe()
    {
        _distance = _touchStart - _touchEnd;

        if (_distance.magnitude >= minDistance)
        {
            CheckDirection();
        }
        else
        {
            return;
        }
    }

    private void CheckDirection()
    {
        swipeType = SwipeType.NONE;

        float x = _distance.x;
        float y = _distance.y;

        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            if(x > 0)
            {
                swipeType = SwipeType.LEFT;
            }
            else
            {
                swipeType = SwipeType.RIGHT;
            }
        }
        else
        {
            if (y > 0)
            {
                swipeType = SwipeType.DOWN;
            }
            else
            {
                swipeType = SwipeType.UP;
            }
        }

        Debug.Log($"Swipe Type = {swipeType}");
        _distance = Vector2.zero;
    }
}


