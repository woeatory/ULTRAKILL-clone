using UnityEngine;

public class SlamState : IPlayerState
{
    public void Enter(PlayerStateMachine context)
    {
        context.playerController.IsSliding = true;
        MoveCharacter(context);
    }

    public void Exit(PlayerStateMachine context) { }

    public void Update(PlayerStateMachine context)
    {
        context.playerController.characterController.Move(context.playerController.playerVelocity * Time.deltaTime);
        CheckIfSwitchState(context);
    }
    public void CheckIfSwitchState(PlayerStateMachine context)
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
    public void MoveCharacter(PlayerStateMachine context)
    {
        context.playerController.playerVelocity = Vector3.zero;
        context.playerController.playerVelocity.y = -context.playerController.slamForce;
    }
}
