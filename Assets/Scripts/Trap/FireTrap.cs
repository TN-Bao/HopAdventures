using System.Collections;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _time = 4f;
    [SerializeField] private Collider2D _fireTrigger;

    private static readonly int ActiveHash = Animator.StringToHash("active");

    private void Start()
    {
        SetFire(false);
        StartCoroutine(FireLoop());
    }

    private IEnumerator FireLoop()
    {
        while (true)
        {
            SetFire(true);
            yield return new WaitForSeconds(_time);

            SetFire(false);
            yield return new WaitForSeconds(_time);
        }
    }

    private void SetFire(bool isOn)
    {
        _animator.SetBool(ActiveHash, isOn);
        _fireTrigger.enabled = isOn;
    }
}
