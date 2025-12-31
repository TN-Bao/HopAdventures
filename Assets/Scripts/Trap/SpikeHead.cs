using UnityEngine;

public class SpikeHead : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 4f;
    [SerializeField] private Vector2 _dir = Vector2.right;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;

    private bool _launched;

    public void LaunchTrap()
    {
        if (_launched) return;

        _launched = true;
        _rb.velocity = _dir.normalized * _moveSpeed;
        PlayAnimation(true);
    }

    private void PlayAnimation(bool isActivated)
    {
        _animator.SetBool("activated", isActivated);
    }

    private void ColliderAnimation()
    {
        _animator.SetTrigger("collider");
        _rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        {
            PlayAnimation(false);
            ColliderAnimation();
        }
    }
}
