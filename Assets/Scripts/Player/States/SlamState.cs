using UnityEngine;

public class SlamState : PlayerState
{
    public override void Enter(PlayerStateMachine context)
    {
        context.playerController.IsSliding = true;
        PerformSlam(context);
    }

    public override void Exit(PlayerStateMachine context) { }

    public override void Update(PlayerStateMachine context)
    {
        context.playerController.characterController.Move(context.playerController.playerVelocity * Time.deltaTime);
        CheckIfSwitchState(context);
    }
    public override void CheckIfSwitchState(PlayerStateMachine context)
    {
        if (context.playerController.IsGrounded && context.playerController.IsSliding)
        {
            context.TransitionTo(new SlideState());
        } 
        else if (context.playerController.IsGrounded)
        {
            context.TransitionTo(new GroundedState());
        } 
    }
    private void PerformSlam(PlayerStateMachine context)
    {
        context.playerController.playerVelocity = Vector3.zero;
        context.playerController.playerVelocity.y = -context.playerController.slamForce;
    }
}
