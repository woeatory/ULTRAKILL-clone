using UnityEngine;

public class PlayerMovementBehaviour : MonoBehaviour
{
    [Header("Components References")]
    [SerializeField] CharacterController characterController;
    [Header("Movement Settings")]
    [SerializeField] float movementSpeed = 10f;
    private Vector3 movementDirection;
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void FixedUpdate()
    {
        MoveThePlayer();
    }
    public void MoveThePlayer()
    {
        characterController.Move(transform.TransformDirection(movementDirection) * movementSpeed * Time.deltaTime);
    }

    public void PerformeJump()
    {

    }


    public void UpdateMovementData(Vector3 movementDirection)
    {
        this.movementDirection = movementDirection;
    }
}
