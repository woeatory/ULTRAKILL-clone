using UnityEngine;

public class SlideState : PlayerState
{
    public override void Enter(PlayerStateMachine context)
    {
        context.playerController.IsSliding = true;
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
        Vector3 slideDirection = context.playerController.transform.forward;
        context.playerController.playerVelocity = slideDirection;
        ApplyGravity(context);
        context.playerController.characterController.Move(context.playerController.playerVelocity * context.playerController.slideSpeed * Time.deltaTime);
    }
    private void ApplyGravity(PlayerStateMachine context)
    {
        if (context.playerController.IsGrounded) { context.playerController.playerVelocity.y = context.playerController.GroundedYAxixVelocity; }
        context.playerController.playerVelocity.y += context.playerController.fallingSpeed * Time.deltaTime;
    }
}
