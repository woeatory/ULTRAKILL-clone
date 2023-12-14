using UnityEngine;

public class JumpState : PlayerState
{
    public override void Enter(PlayerStateMachine context)
    {
        PerformJump(context);
        context.TransitionTo(new AerialState());
    }

    public override void Exit(PlayerStateMachine context) { }

    public override void Update(PlayerStateMachine context) { }

    private void PerformJump(PlayerStateMachine context)
    {
        context.playerController.playerVelocity.y = Mathf.Sqrt(context.playerController.jumpForce * context.playerController.jumpMultiplier * context.playerController.fallingSpeed);
        context.playerController.characterController.Move(context.playerController.playerVelocity * Time.deltaTime);
    }
    public override void CheckIfSwitchState(PlayerStateMachine context) { }

}
