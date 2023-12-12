using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerController playerController;

    public PlayerState CurrentPlayerState { get; set; }
    public event Action<PlayerState> stateChanged;
    
    public PlayerStateMachine(PlayerController playerController)
    {
        this.playerController = playerController;
        Initialize(new GroundedState());
    }
    public void Initialize(PlayerState playerState)
    {
        CurrentPlayerState = playerState;
        playerState.Enter(this);

        // notify other objects that state has changed
        stateChanged?.Invoke(playerState);
    }
    public void TransitionTo(PlayerState nextState)
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