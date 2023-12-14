using UnityEngine;

public class AerialState : PlayerState
{
    public override void Enter(PlayerStateMachine context)
    {

    }

    public override void Exit(PlayerStateMachine context) { }


    public override void Update(PlayerStateMachine context)
    {
        MoveCharacter(context);
        CheckIfSwitchState(context);
    }
    public override void CheckIfSwitchState(PlayerStateMachine context)
    {
        if (context.playerController.characterController.isGrounded)
        {
            context.TransitionTo(new GroundedState());
        }
    }
    private void MoveCharacter(PlayerStateMachine context)
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