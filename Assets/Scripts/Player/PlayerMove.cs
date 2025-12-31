using UnityEngine;
using static PlayerAnimation;

public class PlayerMove : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerEvents _playerEvent;
    [SerializeField] private PlayerAnimation _playerAnimation;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private LayerMask _jumpableGround;
    [SerializeField] private RigidbodyConstraints2D _rigidbodyConstraints;

    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private float _jumpForce = 12f;


    private BoxCollider2D _collGroundChecking;
    private bool _isGround;
    private float _movementInput;
    private bool _canControl = true;

    public float MovementInput => _movementInput;


    private void Awake()
    {
        _collGroundChecking = GetComponent<BoxCollider2D>();
        _rigidbodyConstraints = _rigidbody.constraints;
    }

    private void FixedUpdate()
    {
        float moveX = _movementInput * _moveSpeed;
        _rigidbody.velocity = new Vector2(moveX, _rigidbody.velocity.y);
    }

    private void Update()
    {
        if (!_canControl)
        {
            _playerAnimation.ChangeState(MovementState.idle);
            return;
        }
        
        _isGround = IsGround();
        MovementState state;

        if (Mathf.Abs(_movementInput) > 0.1f)
            state = MovementState.running;
        else
            state = MovementState.idle;

        if (!_isGround && _rigidbody.velocity.y > 0.1f)
            state = MovementState.jumping;

        else if (!_isGround && _rigidbody.velocity.y < -0.1f)
            state = MovementState.falling;

        _playerAnimation.ChangeState(state);
    }

    public void SetMovementInput(Vector2 value)
    {
        if (!_canControl) return;
        _movementInput = value.x;
    }

    public void Jumping()
    {
        if (!_canControl) return ;
        if (!_isGround) return;
        
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _playerEvent.RaiseJumpedAudio();
    }

    private bool IsGround()
    {
        return Physics2D.BoxCast(_collGroundChecking.bounds.center,
            _collGroundChecking.bounds.size, 0f, Vector2.down, .1f, _jumpableGround);
    }

    public void EnableControl()
    {
        _canControl = true;
        _rigidbody.constraints = _rigidbodyConstraints;
    }

    public void DisableControl()
    {
        if (!_canControl) return;

        _canControl = false;
        _movementInput = 0;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
