using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{    
    private Animator _animator;
    public enum MovementState {  idle, running, jumping, falling }
    public event Action OnDieFinished;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeState(MovementState state)
    {
        _animator.SetInteger("State", (int)state);
    }

    public void ChangeAnimationDirection(Vector2 dir)
    {
        if (dir.magnitude < 0.1f) return;

        transform.localScale = (dir.x > 0) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
    }

    public void DieAnimation()
    {
        _animator.SetTrigger("Die");
    }

    public void AnimDieFinished()
    {
        OnDieFinished?.Invoke();
    }
}
