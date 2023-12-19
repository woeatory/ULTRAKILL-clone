using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerLook playerLook;
    private Vector3 rawInputMovement;
    private Vector2 mouseLookDirection;
    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        playerLook = GetComponent<PlayerLook>();
    }
    private void Update()
    {
        UpdatePlayerMovement();
        UpdateMouseLook();
    }

    private void UpdatePlayerMovement()
    {
        playerController.UpdatePlayerMovement(rawInputMovement);
    }
    private void UpdateMouseLook()
    {
        playerLook.UpdateMousePosition(mouseLookDirection);
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 inputMovement = context.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Jump pressed");
            if (playerController.characterController.isGrounded)
            {
                playerController.PlayerStateMachine.TransitionTo(new JumpState());
            }
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        playerController.PerformDash();
    }
    public void OnSlide(InputAction.CallbackContext context)
    {
        if (playerController.PlayerStateMachine.CurrentPlayerState is AerialState)
        {
            playerController.PlayerStateMachine.TransitionTo(new SlamState());
        }
        else if (playerController.IsGrounded)
        {
            playerController.PlayerStateMachine.TransitionTo(new SlideState());
        }
        if (context.canceled)
        {
            playerController.IsSliding = false;
        }
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 inputLook = context.ReadValue<Vector2>();
        mouseLookDirection = inputLook;
    }
}
