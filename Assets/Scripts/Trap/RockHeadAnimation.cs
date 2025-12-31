using UnityEngine;

public class RockHeadAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _animator.SetBool("blink", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _animator.SetBool("blink", false);
        }
    }
}
