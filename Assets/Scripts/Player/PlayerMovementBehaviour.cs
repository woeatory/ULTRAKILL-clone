using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovementBehaviour : MonoBehaviour
{
    [Header("Components References")]
    [SerializeField] private CharacterController characterController;
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 10f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    public void OnMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        characterController.Move(transform.TransformDirection(moveDirection) * movementSpeed * Time.deltaTime);
    }
}
