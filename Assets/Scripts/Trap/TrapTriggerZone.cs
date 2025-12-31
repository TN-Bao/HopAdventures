using UnityEngine;

public class TrapTriggerZone : MonoBehaviour
{
    [SerializeField] private SpikeHead _trap;

    private bool _isTriggered;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isTriggered) return;
        if (!collision.CompareTag("Player")) return;

        _isTriggered = true;
        _trap.LaunchTrap();
    }
}
