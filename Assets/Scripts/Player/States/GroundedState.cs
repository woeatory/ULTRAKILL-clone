using UnityEngine;

public class GroundedState : PlayerState
{
    public override void Enter(PlayerStateMachine context)
    {
        context.playerController.playerVelocity.y = context.playerController.GroundedYAxixVelocity;
    }

    public override void Exit(PlayerStateMachine context) { }

    public override void Update(PlayerStateMachine context)
    {
        CheckIfSwitchState(context);
        MoveCharacter(context);
    }
    public override void CheckIfSwitchState(PlayerStateMachine context)
    {
        if (!context.playerController.IsGrounded)
        {
            context.TransitionTo(new AerialState());
        }
    }
    private void MoveCharacter(PlayerStateMachine context)
    {
        if (!context.playerController.IsDashing)
        {
            context.playerController.playerVelocity.x = context.playerController.movementDirection.x * context.playerController.movementSpeed;
            context.playerController.playerVelocity.z = context.playerController.movementDirection.z * context.playerController.movementSpeed;
        }

        context.playerController.characterController.Move(context.playerController.playerVelocity * Time.deltaTime);
    }
}