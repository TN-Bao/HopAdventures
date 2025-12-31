using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private IPlayerState _currentState;

    public void ChangeState(IPlayerState nextState)
    {
        if (_currentState is PlayerDeadState && nextState is PlayerDeadState) return;
        
        _currentState?.Exit();
        _currentState = nextState;
        _currentState?.Enter();
    }

    private void Update()
    {
        _currentState?.Tick();
    }
}
