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
    private void Update()
    {
        MoveThePlayer();
    }
    public void MoveThePlayer()
    {
        characterController.Move(movementDirection * movementSpeed * Time.deltaTime);
    }
    public void UpdateMovementData(Vector3 movementDirection)
    {
        this.movementDirection = transform.right * movementDirection.x + transform.forward * movementDirection.z;
    }
    public void PerformeJump()
    {

    }
}
