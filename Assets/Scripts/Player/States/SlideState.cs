using UnityEngine;

public class SlideState : PlayerState
{
    private Vector3 slideDirection;
    public override void Enter(PlayerStateMachine context)
    {
        context.playerController.IsSliding = true;
        slideDirection = context.playerController.transform.forward;
    }

    public override void Exit(PlayerStateMachine context) { }

    public override void Update(PlayerStateMachine context)
    {
        CheckIfSwitchState(context);
        PerformSlide(context);
    }

    public override void CheckIfSwitchState(PlayerStateMachine context)
    {
        if (!context.playerController.IsSliding)
        {
            context.TransitionTo(new GroundedState());
        }
    }
    private void PerformSlide(PlayerStateMachine context)
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
