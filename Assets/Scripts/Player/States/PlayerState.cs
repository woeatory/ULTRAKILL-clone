public interface IPlayerState
{
    public void Enter(PlayerStateMachine context);
    public void Exit(PlayerStateMachine context);
    public void Update(PlayerStateMachine context);
    public void MoveCharacter(PlayerStateMachine context);
    public void CheckIfSwitchState(PlayerStateMachine context);
}