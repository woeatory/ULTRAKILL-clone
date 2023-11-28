using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] PlayerMovementBehaviour playerMovementBehaviour;
    [SerializeField] PlayerLook playerLook;
    private Vector3 rawInputMovement;
    private Vector2 mouseLookDirection;
    private void Start()
    {
        playerMovementBehaviour = GetComponent<PlayerMovementBehaviour>();
        playerLook = GetComponent<PlayerLook>();
    }
    private void Update()
    {
        UpdatePlayerMovement();
        UpdateMouseLook();
    }

    private void UpdatePlayerMovement()
    {
        playerMovementBehaviour.UpdateMovementData(rawInputMovement);
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
        playerMovementBehaviour.PerformeJump();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        playerMovementBehaviour.PerformeDash();
    }
    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 inputLook = context.ReadValue<Vector2>();
        mouseLookDirection = inputLook;
    }
}
