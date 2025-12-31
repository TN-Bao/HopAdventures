public class PlayerCompletedState : PlayerState
{
    private bool _done;

    public PlayerCompletedState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        _player.SetInputEnable(false);
        _player.SetControlEnable(false);
        _done = false;

        _player.PlayerEvents.RaiseWonAudio();
    }

    public override void Tick()
    {
        if (_done) return;
        _done = true;

        _player.PlayerEvents.RaiseLevelCompleted(new LevelResult
        {
            _levelIndex = _player.LevelIndex,
            _collectedFruit = _player.CollectedFruits,
            _totalFruit = _player.TotalFruits,
            _currentLives = _player.PlayerLife.CurrentLives,
            _maxLives = _player.PlayerLife.MaxLives,
            _stars = 0
        }); 
    }
}
