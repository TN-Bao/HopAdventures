public class PlayerPauseState : PlayerState
{
    public PlayerPauseState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        _player.SetInputEnable(false);
        _player.SetControlEnable(false);

        _player.PlayerEvents.RaisePauseGame();
    }
}
