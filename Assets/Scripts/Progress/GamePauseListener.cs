using UnityEngine;

public class GamePauseListener : MonoBehaviour
{
    [Header("Listen To")]
    [SerializeField] private PlayerEvents _playerEvents;

    [Header("UI Data")]
    [SerializeField] private GUIManager _guiManager;

    private void OnEnable()
    {
        if (_playerEvents != null)
            _playerEvents.OnPause += PauseGame;
    }

    private void PauseGame()
    {
        if (_guiManager != null)
            _guiManager.ShowPausedDialog();
    }

    private void OnDisable()
    {
        if (_playerEvents != null)
            _playerEvents.OnPause -= PauseGame;
    }
}
