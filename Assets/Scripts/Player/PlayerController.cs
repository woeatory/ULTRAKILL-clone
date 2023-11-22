using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] PlayerMovementBehaviour playerMovementBehaviour;
    private PlayerControls playerControls;
    private PlayerControls.GameplayActions gameplay;
    private Vector3 rawInputMovement;
    private void Awake()
    {
        playerControls = new PlayerControls();
        gameplay = playerControls.gameplay;
        playerMovementBehaviour = GetComponent<PlayerMovementBehaviour>();
        // add callback methods, when it is perfomed
        gameplay.Jump.performed += OnJump;
        gameplay.Movement.performed += OnMovement;
    }
    private void Update()
    {
        UpdatePlayerMovement();
    }
    
    private void UpdatePlayerMovement()
    {
        playerMovementBehaviour.UpdateMovementData(rawInputMovement);
    }
    private void OnMovement(InputAction.CallbackContext context)
    {
        Debug.Log("OnMovement");
        Vector2 inputMovement = context.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
    }
    private void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("OnJump");
        if (context.performed) { playerMovementBehaviour.PerformeJump(); }
    }
    private void OnEnable()
    {
        gameplay.Enable();
    }

    private void OnDisable()
    {
        gameplay.Disable();
    }
}
