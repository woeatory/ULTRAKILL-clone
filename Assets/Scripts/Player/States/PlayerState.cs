public abstract class PlayerState
{
    public abstract void Enter(PlayerStateMachine context);
    public abstract void Exit(PlayerStateMachine context);
    public abstract void Update(PlayerStateMachine context);
}
