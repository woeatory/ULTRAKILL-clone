using System;

public class PlayerStateMachine
{
    public PlayerController playerController;

    public IPlayerState CurrentPlayerState { get; set; }
    public event Action<IPlayerState> stateChanged;
    
    public PlayerStateMachine(PlayerController playerController)
    {
        this.playerController = playerController;
        Initialize(new GroundedState());
    }
    public void Initialize(IPlayerState playerState)
    {
        CurrentPlayerState = playerState;
        playerState.Enter(this);

        // notify other objects that state has changed
        stateChanged?.Invoke(playerState);
    }
    public void TransitionTo(IPlayerState nextState)
    {
        CurrentPlayerState.Exit(this);
        CurrentPlayerState = nextState;
        nextState.Enter(this);

        // notify other objects that state has changed
        stateChanged?.Invoke(nextState);
    }
    public void Update()
    {
        CurrentPlayerState?.Update(this);
    }
}