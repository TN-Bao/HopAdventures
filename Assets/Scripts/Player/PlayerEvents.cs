using System;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public event Action<int, int> OnFruitChanged;
    public event Action<int, int> OnLivesChanged;
    public event Action<LevelResult> OnLevelCompleted;
    public event Action OnGameOver, OnPause, OnResume;

    public event Action OnJumpedAudio;
    public event Action OnCollectedAudio;
    public event Action OnDamagedAudio;
    public event Action OnDiedAudio;
    public event Action OnWonAudio;

    public void RaiseFruitChanged(int collected, int total)
        => OnFruitChanged?.Invoke(collected, total);

    public void RaiseLivesChanged(int current, int max)
        => OnLivesChanged?.Invoke(current, max);

    public void RaiseLevelCompleted(LevelResult result)
        => OnLevelCompleted?.Invoke(result);

    public void RaiseGameOver() => OnGameOver?.Invoke();
    public void RaisePauseGame() => OnPause?.Invoke();
    public void RaiseResumeGame() => OnResume?.Invoke();
    public void RaiseJumpedAudio() => OnJumpedAudio?.Invoke();
    public void RaiseCollectedAudio() => OnCollectedAudio?.Invoke();
    public void RaiseDamagedAudio() => OnDamagedAudio?.Invoke();
    public void RaiseDiedAudio() => OnDiedAudio?.Invoke();
    public void RaiseWonAudio() => OnWonAudio?.Invoke();
}

public struct LevelResult
{
    public int _levelIndex;
    public int _collectedFruit;
    public int _totalFruit;
    public int _currentLives;
    public int _maxLives;
    public int _stars;
}
