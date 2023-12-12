using UnityEngine;

public class GroundedState : PlayerState
{
    public override void Enter(PlayerStateMachine context)
    {
        context.playerController.movementDirection.y = 0;
    }

    public override void Exit(PlayerStateMachine context) { }

    public override void Update(PlayerStateMachine context)
    {
        CheckIfSwitchState(context);
        MoveCharacter(context);
    }
    public override void CheckIfSwitchState(PlayerStateMachine context)
    {
        if (!context.playerController.characterController.isGrounded)
        {
            context.TransitionTo(new AerialState());
        }
    }
    private void MoveCharacter(PlayerStateMachine context)
    {
        if (!context.playerController.IsDashing)
        {
            context.playerController.movementVelocity.x = context.playerController.movementDirection.x * context.playerController.movementSpeed;
            context.playerController.movementVelocity.z = context.playerController.movementDirection.z * context.playerController.movementSpeed;
        }

        context.playerController.characterController.Move(context.playerController.movementVelocity * Time.deltaTime);
    }
}