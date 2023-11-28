using UnityEngine;

public class PlayerMovementBehaviour : MonoBehaviour
{
    [Header("Components References")]
    [SerializeField] CharacterController characterController;
    [Header("Movement Settings")]
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float fallingSpeed = -4.5f;
    private float _playerYAxisVelocity;
    private Vector3 _movementDirection;
    private bool isGrounded;

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
        characterController.Move(_movementDirection * Time.deltaTime);
    }
    public void UpdateMovementData(Vector3 movementDirection)
    {
        _movementDirection = transform.right * movementDirection.x + transform.forward * movementDirection.z;
    }
    public void PerformeJump()
    {

    }

    private void ApplyGravity()
    {
        isGrounded = characterController.isGrounded;
        Debug.Log(isGrounded);
        if (isGrounded && _playerYAxisVelocity < 0)
        {
            _playerYAxisVelocity = -2f;
        } 
        _playerYAxisVelocity = fallingSpeed;
        _movementDirection.y = _playerYAxisVelocity;
    }
}
