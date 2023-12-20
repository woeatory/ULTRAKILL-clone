using UnityEngine;

public class SlideState : IPlayerState
{
    private Vector3 slideDirection;
    public void Enter(PlayerStateMachine context)
    {
        context.playerController.IsSliding = true;
        slideDirection = context.playerController.transform.forward;
    }

    public void Exit(PlayerStateMachine context) { }

    public void Update(PlayerStateMachine context)
    {
        CheckIfSwitchState(context);
        MoveCharacter(context);
    }

    public void CheckIfSwitchState(PlayerStateMachine context)
    {
        if (!context.playerController.IsSliding)
        {
            context.TransitionTo(new GroundedState());
        }
    }
    public void MoveCharacter(PlayerStateMachine context)
    {
        context.playerController.playerVelocity = slideDirection;
        ApplyGravity(context);
        context.playerController.characterController.Move(context.playerController.slideSpeed * Time.deltaTime * context.playerController.playerVelocity);
    }
    private void ApplyGravity(PlayerStateMachine context)
    {
        if (context.playerController.IsGrounded) { context.playerController.playerVelocity.y = context.playerController.GroundedYAxixVelocity; }
        context.playerController.playerVelocity.y += context.playerController.fallingSpeed * Time.deltaTime;
    }
}
