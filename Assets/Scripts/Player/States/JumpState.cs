using UnityEngine;

public class JumpState : PlayerState
{
    public override void Enter(PlayerStateMachine context)
    {
        PerformeJump(context);
        context.TransitionTo(new AerialState());
    }

    public override void Exit(PlayerStateMachine context) { }

    public override void Update(PlayerStateMachine context) { }

    private void PerformeJump(PlayerStateMachine context)
    {
        context.playerController.playerVelocity.y = Mathf.Sqrt(context.playerController.jumpForce * context.playerController.jumpMultiplier * context.playerController.fallingSpeed);
    }
    public override void CheckIfSwitchState(PlayerStateMachine context) { }

}
