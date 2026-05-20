using UnityEngine;
using UnityEngine.InputSystem;

public class TouchInput : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;

    private InputAction _touchPressedAction;
    private InputAction _touchPositionAction;

    private Vector2 _touchStart;
    private Vector2 _touchEnd;

    private void Awake()
    {
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

        if (_touchEnd.x < _touchStart.x)
        {
            Debug.Log("Player Swiped Left");
        }
        else if (_touchEnd.x > _touchStart.x)
        {
            Debug.Log("Player Swiped Right");
        }
    }

}
