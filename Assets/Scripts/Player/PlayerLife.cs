using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private GameData _gameData;
    public int CurrentLives => _gameData.currentLives;
    public int MaxLives => _gameData.maxLives;


    public void TakeDamage()
    {
        _gameData.currentLives--;

        if (_gameData.currentLives < 0)
        {
            _gameData.currentLives = 0;
        }
    }

    public void ResetLives()
    {
        _gameData.currentLives = _gameData.maxLives;
    }
}
