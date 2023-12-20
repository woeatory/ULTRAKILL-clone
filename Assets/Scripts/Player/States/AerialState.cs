using UnityEngine;

public class AerialState : IPlayerState
{
    public void Enter(PlayerStateMachine context) {}

    public void Exit(PlayerStateMachine context)
    {
        context.playerController.playerVelocity.y = context.playerController.GroundedYAxixVelocity;
    }


    public void Update(PlayerStateMachine context)
    {
        CheckIfSwitchState(context);
        MoveCharacter(context);
    }
    public void CheckIfSwitchState(PlayerStateMachine context)
    {
        if (context.playerController.IsGrounded)
        {
            context.TransitionTo(new GroundedState());
        }
        else if (context.playerController.characterController.collisionFlags == CollisionFlags.Sides)
        {
            // context.TransitionTo(new WallSlideState());
        }
    }
    public void MoveCharacter(PlayerStateMachine context)
    {
        if (!context.playerController.IsDashing)
        {
            context.playerController.playerVelocity.x = context.playerController.movementDirection.x * context.playerController.movementSpeed;
            context.playerController.playerVelocity.z = context.playerController.movementDirection.z * context.playerController.movementSpeed;
        }
        context.playerController.playerVelocity.y += context.playerController.fallingSpeed * Time.deltaTime;
        context.playerController.characterController.Move(context.playerController.playerVelocity * Time.deltaTime);
    }
}