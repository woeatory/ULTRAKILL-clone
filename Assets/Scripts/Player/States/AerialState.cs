using UnityEngine;

public class AerialState : PlayerState
{
    public override void Enter(PlayerStateMachine context)
    {

    }

    public override void Exit(PlayerStateMachine context) { }


    public override void Update(PlayerStateMachine context)
    {

        CheckIfSwitchState(context);
        MoveCharacter(context);
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
            context.playerController.movementVelocity.x = context.playerController.movementDirection.x * context.playerController.movementSpeed;
            context.playerController.movementVelocity.z = context.playerController.movementDirection.z * context.playerController.movementSpeed;
        }
        context.playerController.movementVelocity.y += context.playerController.fallingSpeed * Time.deltaTime;
        context.playerController.characterController.Move(context.playerController.movementVelocity * Time.deltaTime);
    }
}