using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Rigidbody2D _rigidBody;
    private float _leftX, _rightX;
    private int _dir;


    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        
        if (!_spriteRenderer)
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        _leftX = Mathf.Min(_pointA.position.x, _pointB.position.x);
        _rightX = Mathf.Max(_pointA.position.x, _pointB.position.x);

        _dir = (_rigidBody.position.x < _rightX) ? 1 : -1;
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity = new Vector2(_dir * _moveSpeed, _rigidBody.velocity.y);

        if (_rigidBody.position.x >= _rightX) _dir = -1;
        else if (_rigidBody.position.x <= _leftX) _dir = 1;

        if (_spriteRenderer && _leftX != _rightX)
            _spriteRenderer.flipX = _dir < 0;
    }
}
