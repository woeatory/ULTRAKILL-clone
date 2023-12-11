using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public CharacterController characterController;
    public PlayerController playerController;
    [Header("Movement Settings")]
    [SerializeField] float movementSpeed = 10f;
    public float fallingSpeed = -4.5f;
    public float jumpForce = 2f;

    public Vector3 movementDirection = Vector3.zero;
    public PlayerState CurrentPlayerState { get; set; }
    public event Action<PlayerState> stateChanged;

    // getters/setters
    public float MovementSpeed => movementSpeed;
    
    public PlayerStateMachine(CharacterController characterController, PlayerController playerController)
    {
        this.characterController = characterController;
        this.playerController = playerController;
        Initialize(new WalkState());
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
        if (CurrentPlayerState != null)
        {
            CurrentPlayerState.Update(this);
        }
    }
}