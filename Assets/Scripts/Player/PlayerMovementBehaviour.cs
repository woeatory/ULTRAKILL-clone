using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerMovementBehaviour : MonoBehaviour
{
    [Header("Components References")]
    [SerializeField] CharacterController characterController;
    [Header("Movement Settings")]
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float fallingSpeed = -4.5f;
    [SerializeField] float jumpForce = 2f;
    [SerializeField] float dashDistance = 15f;
    [SerializeField] float dashDuration = 0.35f;
    [SerializeField] float dashCounterResetTime = 1.75f;
    private int dashCounter = 3;
    private float _playerYAxisVelocity;
    private Vector3 _movementDirection;
    private bool isDashing = false;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        ApplyGravity();
        MovePlayer();
    }
    private void MovePlayer()
    {
        _movementDirection.x *= movementSpeed;
        _movementDirection.z *= movementSpeed;
        _movementDirection.y = _playerYAxisVelocity;
        characterController.Move(_movementDirection * Time.deltaTime);
    }
    public void UpdateMovementData(Vector3 movementDirection)
    {
        _movementDirection = transform.right * movementDirection.x + transform.forward * movementDirection.z;
    }
    public void PerformeJump()
    {
        if (characterController.isGrounded)
        {
            _playerYAxisVelocity = Mathf.Sqrt(jumpForce * -3.0f * fallingSpeed);
        }
    }
    public void PerformeDash()
    {
        // todo: improve charge reset
        if (isDashing || dashCounter == 0) { return; }
        isDashing = true;
        dashCounter--;
        var dashDirection = new Vector3(_movementDirection.x, 0f, _movementDirection.z);
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
        isDashing = false;
    }
    private void ResetDashCounter()
    {
        dashCounter++;
    }
    private void ApplyGravity()
    {
        if (characterController.isGrounded && _playerYAxisVelocity < 0)
        {
            _playerYAxisVelocity = -2f;
        }
        _playerYAxisVelocity += fallingSpeed * Time.deltaTime;
    }
}
