using UnityEngine.SceneManagement;

public class PlayerDeadState : PlayerState
{
    private bool _animDieFinished;
    
    public PlayerDeadState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        _animDieFinished = false;
        
        _player.SetInputEnable(false);
        _player.SetControlEnable(false);

        _player.PlayerLife.TakeDamage();
        _player.PlayerEvents.RaiseLivesChanged(_player.PlayerLife.CurrentLives, _player.PlayerLife.MaxLives);

        _player.PlayerEvents.RaiseDamagedAudio();

        _player.PlayerAnimation.OnDieFinished += DieFinished;
        _player.PlayerAnimation.DieAnimation();
    }

    private void DieFinished()
    {
        if (_animDieFinished) return;
        _animDieFinished = true;

        _player.PlayerAnimation.OnDieFinished -= DieFinished;

        if (_player.PlayerLife.CurrentLives <= 0)
        {
            _player.PlayerEvents.RaiseGameOver();
            _player.PlayerEvents.RaiseDiedAudio();
            return;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public override void Exit()
    {
        _player.PlayerAnimation.OnDieFinished -= DieFinished;
    }
}
