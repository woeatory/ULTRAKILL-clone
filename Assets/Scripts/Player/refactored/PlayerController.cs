using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputManager), typeof(PlayerInputManager), typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] PlayerInputManager playerInputManager;
    [SerializeField] PlayerLook playerLook;
    private PlayerStateMachine playerStateMachine;
    // Input
    private Vector3 movementDirection;
    public float PlayerYAxisVelocity {get; set;}
    private Vector2 mouseLookDirection;
    [Header("Movement Settings")]
    [SerializeField] float movementSpeed = 10f;
    public float fallingSpeed = -4.5f;
    public float jumpForce = 2f;
    public CharacterController CharController => characterController;
    public PlayerStateMachine PlayerStateMachine => playerStateMachine;

    private CharacterController characterController;
    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        playerLook = GetComponent<PlayerLook>();
        characterController = GetComponent<CharacterController>();
        playerStateMachine = new PlayerStateMachine(characterController, this);
    }

    private void Update()
    {
        playerStateMachine.Update();

    }
    private void LateUpdate()
    {
        CalculateVertical();
        Move();
    }
    public void UpdatePlayerMovement(Vector3 inputMovement)
    {
        movementDirection = inputMovement;
        movementDirection = transform.right * movementDirection.x + transform.forward * movementDirection.z;
    }
    public void PerformeJump() 
    {
        playerStateMachine.TransitionTo(new JumpState());
    }
    private void Move()
    {
        movementDirection.x *= movementSpeed;
        movementDirection.z *= movementSpeed;
        movementDirection.y = PlayerYAxisVelocity;
        characterController.Move(movementDirection * Time.deltaTime);
    }

    private void CalculateVertical()
    {
        if (characterController.isGrounded && PlayerYAxisVelocity < 0)
        {
            PlayerYAxisVelocity = -2f;
        }
        PlayerYAxisVelocity += fallingSpeed * Time.deltaTime;
    }
    
}