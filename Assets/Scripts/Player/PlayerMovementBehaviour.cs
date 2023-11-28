using UnityEngine;

public class PlayerMovementBehaviour : MonoBehaviour
{
    [Header("Components References")]
    [SerializeField] CharacterController characterController;
    [Header("Movement Settings")]
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float fallingSpeed = -4.5f;
    [SerializeField] float jumpForce = 1f;
    private float _playerYAxisVelocity;
    private Vector3 _movementDirection;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        ApplyGravity();
        MoveThePlayer();
    }
    private void MoveThePlayer()
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

    private void ApplyGravity()
    {
        if (characterController.isGrounded && _playerYAxisVelocity < 0)
        {
            _playerYAxisVelocity = -2f;
        }
        _playerYAxisVelocity += fallingSpeed * Time.deltaTime;
    }
}
