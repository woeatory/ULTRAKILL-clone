using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("Camera Sensitivity Settings")]
    [SerializeField] float xSensitivity = 5f;
    [SerializeField] float ySensitivity = 5f;
    private Camera _camera;
    private float xMousePos, yMousePos, xRotation = 0f;
    private const float MIN_CLAMP = -90f;
    private const float MAX_CLAMP = 90f;

    private void Start()
    {
        _camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        RotateCamera();
    }

    public void RotateCamera()
    {
        _camera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * xMousePos);
    }
    public void UpdateMousePosition(Vector2 mouseInput)
    {
        xMousePos = mouseInput.x * xSensitivity * Time.deltaTime;
        yMousePos = mouseInput.y * ySensitivity * Time.deltaTime;
        xRotation -= yMousePos;
        xRotation = Mathf.Clamp(xRotation, MIN_CLAMP, MAX_CLAMP);
    }
}
