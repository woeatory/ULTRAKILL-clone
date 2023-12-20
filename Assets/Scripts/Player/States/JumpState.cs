using UnityEngine;

public class JumpState : IPlayerState
{
    public void Enter(PlayerStateMachine context)
    {
        MoveCharacter(context);
        context.TransitionTo(new AerialState());
    }

    public void Exit(PlayerStateMachine context) { }
    public void Update(PlayerStateMachine context) { }
    public void MoveCharacter(PlayerStateMachine context)
    {
        context.playerController.playerVelocity.y = Mathf.Sqrt(context.playerController.jumpForce * context.playerController.jumpMultiplier * context.playerController.fallingSpeed);
        context.playerController.characterController.Move(context.playerController.playerVelocity * Time.deltaTime);
    }

    public void CheckIfSwitchState(PlayerStateMachine context) { }
}