using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [Header("Camera Sensitivity Settings")]
    [SerializeField] float xSensitivity = 5f;
    [SerializeField] float ySensitivity = 5f;
    private Camera _camera;
    private float _xMousePos, _yMousePos, _xRotation = 0f;
    private const float MIN_CLAMP = -90f;
    private const float MAX_CLAMP = 90f;

    private void Start()
    {
        _camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        RotateCamera();
    }

    public void RotateCamera()
    {
        _camera.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * _xMousePos);
    }
    public void UpdateMousePosition(Vector2 mouseInput)
    {
        _xMousePos = mouseInput.x * xSensitivity * Time.deltaTime;
        _yMousePos = mouseInput.y * ySensitivity * Time.deltaTime;
        _xRotation -= _yMousePos;
        _xRotation = Mathf.Clamp(_xRotation, MIN_CLAMP, MAX_CLAMP);
    }
}
