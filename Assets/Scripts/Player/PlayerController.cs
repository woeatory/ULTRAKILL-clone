using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private PlayerMovementBehaviour playerMovementBehaviour;
    private PlayerControls playerControls;
    private PlayerControls.GameplayActions gameplay;

    private void Awake()
    {
        playerControls = new PlayerControls();
        gameplay = playerControls.gameplay;
        playerMovementBehaviour = GetComponent<PlayerMovementBehaviour>();
    }

    private void FixedUpdate()
    {
        playerMovementBehaviour.OnMove(gameplay.Movement.ReadValue<Vector2>());
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
