public class PlayerNormalState : PlayerState
{
    public PlayerNormalState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        _player.SetInputEnable(true);
        _player.SetControlEnable(true);
    }
}
