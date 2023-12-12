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
    // Movement
    private Vector3 movementDirection;
    private Vector3 movementVelocity;
    public float PlayerYAxisVelocity { get; set; }

    private Vector2 mouseLookDirection;
    [Header("Movement Settings")]
    [SerializeField] float movementSpeed = 10f;
    public float fallingSpeed = -4.5f;
    public float jumpForce = 2f;
    public CharacterController CharController => characterController;
    public PlayerStateMachine PlayerStateMachine => playerStateMachine;
    public bool IsDashing { get; set; }
    public int dashCounter = 3;
    [SerializeField] float dashDuration = 0.35f;
    [SerializeField] float dashDistance = 25f;
    [SerializeField] float dashCounterResetTime = 1.75f;
    [SerializeField] Vector3 dashDirection;
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
    public void PerformJump()
    {
        playerStateMachine.TransitionTo(new JumpState());
    }
    private void Move()
    {
        if (!IsDashing)
        {
            movementVelocity.x = movementDirection.x * movementSpeed;
            movementVelocity.z = movementDirection.z * movementSpeed;
        }
        movementVelocity.y = PlayerYAxisVelocity;
        characterController.Move(movementVelocity * Time.deltaTime);
    }

    private void CalculateVertical()
    {
        if (characterController.isGrounded && PlayerYAxisVelocity < 0)
        {
            PlayerYAxisVelocity = -2f;
        }
        PlayerYAxisVelocity += fallingSpeed * Time.deltaTime;
    }

    public void PerformDash()
    {
        // todo: improve charge reset
        if (IsDashing || dashCounter == 0) { return; }
        IsDashing = true;
        dashCounter--;
        dashDirection = new Vector3(movementDirection.x, 0f, movementDirection.z);
        if (dashDirection == Vector3.zero)
        {
            dashDirection = transform.forward;
        }
        StartCoroutine(Dash(dashDirection));
        Invoke("ResetDashCounter", dashCounterResetTime);
    }
    private IEnumerator Dash(Vector3 dashDirection)
    {
        float startTime = Time.time;
        while (Time.time < dashDuration + startTime)
        {
            characterController.Move(dashDirection * dashDistance * Time.deltaTime);
            yield return null;
        }
        IsDashing = false;
    }
    private void ResetDashCounter()
    {
        dashCounter++;
    }
}