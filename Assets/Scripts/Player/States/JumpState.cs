using UnityEngine;

public class JumpState : PlayerState
{
    public override void Enter(PlayerStateMachine context)
    {
        PerformeJump(context);
    }

    public override void Exit(PlayerStateMachine context)
    {
        
    }

    public override void Update(PlayerStateMachine context)
    {
        if (context.characterController.isGrounded)
        {
            context.TransitionTo(new WalkState());
        } 
    }

    private void PerformeJump(PlayerStateMachine context)
    {
        context.playerController.PlayerYAxisVelocity = Mathf.Sqrt(context.playerController.jumpForce * -3.0f * context.playerController.fallingSpeed);
    }

}
