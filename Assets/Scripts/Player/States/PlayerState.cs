public abstract class PlayerState : IPlayerState
{
    protected readonly Player _player;
    protected PlayerState(Player player) => _player = player;


    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Tick() { }
}
