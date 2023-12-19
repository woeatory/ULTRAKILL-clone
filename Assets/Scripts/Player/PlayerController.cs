using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Component References")]
    private PlayerStateMachine playerStateMachine;
    public CharacterController characterController;
    public PlayerLook playerLook;
    // Movement
    public Vector3 movementDirection;
    public Vector3 playerVelocity;

    private Vector2 mouseLookDirection;
    [Header("Movement Settings")]
    public float movementSpeed = 10f;
    public float fallingSpeed = -4.5f;
    public float jumpForce = 2f;
    public float jumpMultiplier = -3f;
    public float slamForce = 25f;
    public float slideSpeed = 20f;
    public float GroundedYAxixVelocity {get;} = -2f;
    public PlayerStateMachine PlayerStateMachine => playerStateMachine;
    public bool IsGrounded => characterController.isGrounded;
    [SerializeField] public bool IsDashing { get; set; }
    [SerializeField] public bool IsSliding;
    public int dashCounter = 3;
    [SerializeField] float dashDuration = 0.35f;
    [SerializeField] float dashDistance = 25f;
    [SerializeField] float dashCounterResetTime = 1.75f;
    [SerializeField] Vector3 dashDirection;

    private void Awake()
    {
        playerLook = GetComponent<PlayerLook>();
        characterController = GetComponent<CharacterController>();
        playerStateMachine = new PlayerStateMachine(this);
    }

    private void Update()
    {
        playerStateMachine.Update();
    }
    public void UpdatePlayerMovement(Vector3 inputMovement)
    {
        movementDirection = inputMovement;
        movementDirection = transform.right * movementDirection.x + transform.forward * movementDirection.z;
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