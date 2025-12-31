using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInputGame : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [field: SerializeField] private Vector2 InputValue { get; set; }

    public UnityEvent<Vector2> OnMoveInput;
    public event Action OnJump;
    public event Action OnPause;

    private InputAction _moveAction, _jumpAction, _pauseAction;

    private void Awake()
    {
        if (_playerInput == null)
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        if (_playerInput == null)
        {
            Debug.LogError("[Player] missing PlayerInput reference", this);
            return;
        }

        _moveAction = _playerInput.actions["Player/Movement"];
        _jumpAction = _playerInput.actions["Player/Jumping"];
        _pauseAction = _playerInput.actions["Player/Pausing"];
    }

    private void OnEnable()
    {
        _moveAction.performed += Move;
        _moveAction.canceled += Move;

        _jumpAction.performed += Jump;
        _pauseAction.performed += Pause;
    }

    public void SetGamePlayInput(bool enabled)
    {
        if (enabled)
        {
            _moveAction.Enable();
            _jumpAction.Enable();
        }
        else
        {
            _moveAction.Disable();
            _jumpAction.Disable();
            OnMoveInput?.Invoke(Vector2.zero);
        }
    }

    private void Pause(InputAction.CallbackContext context)
    {
        OnPause?.Invoke();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }

    private void Move(InputAction.CallbackContext context)
    {
        if (context.canceled)
            InputValue = Vector2.zero;
        else
            InputValue = context.ReadValue<Vector2>();

        OnMoveInput?.Invoke(InputValue);
    }

    private void OnDisable()
    {
        _moveAction.performed -= Move;
        _moveAction.canceled -= Move;

        _jumpAction.performed -= Jump;
        _pauseAction.performed -= Pause;
    }
}
