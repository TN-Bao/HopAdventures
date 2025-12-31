using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;
    [SerializeField] private float _moveSpeed = 4f;

    private float _t;
    private bool _moveForward = true;

    private void Update()
    {
        _t += (_moveForward ? 1 : -1) * _moveSpeed * Time.deltaTime;

        if (_t >= 1f)
        {
            _t = 1f;
            _moveForward = false;
        }
        else if (_t <= 0f)
        {
            _t = 0f;
            _moveForward = true;
        }

        transform.position = Vector3.Lerp(_pointA.position, _pointB.position, _t);
    }
}
