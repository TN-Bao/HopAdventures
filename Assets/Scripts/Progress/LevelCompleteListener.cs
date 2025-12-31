using UnityEngine;

public class LevelCompleteListener : MonoBehaviour
{
    [Header("Listen To")]
    [SerializeField] private PlayerEvents _playerEvents;

    [Header("UI Data")]
    [SerializeField] private GUIManager _guiManager;
    [SerializeField] private CompletedDialog _completedDialog;
    [SerializeField] private LevelProgressData _progressData;

    private void OnEnable()
    {
        if (_playerEvents != null)
            _playerEvents.OnLevelCompleted += LevelCompleted;
    }

    private void LevelCompleted(LevelResult result)
    {
        int stars = 0;
        if (_completedDialog != null)
        {
            stars = _completedDialog.SetStarResult(
                result._collectedFruit, result._totalFruit,
                result._currentLives, result._maxLives);
        }

        if (_progressData != null)
            _progressData.SetResult(result._levelIndex, stars);

        if (_guiManager != null)
            _guiManager.ShowCompeletedDialog();
    }

    private void OnDisable()
    {
        if (_playerEvents != null)
            _playerEvents.OnLevelCompleted -= LevelCompleted;
    }
}
