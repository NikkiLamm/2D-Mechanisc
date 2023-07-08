using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveToAD : MonoBehaviour
{
    private ControllerToAD input = null;
    private Vector2 moveVector = Vector2.zero;
    private Rigidbody2D rb = null;
    [SerializeField] private float moveSpeed = 10.0f;

    private void Awake()
    {
        input = new ControllerToAD();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        input.Enable();
        input.Player.MoveToAd.performed += OnMovementPerformed;
        input.Player.MoveToAd.canceled += OnMovementPerformed;
    }

    private void OnDisable()
    {
        input.Disable();
        input.Player.MoveToAd.performed -= OnMovementCancelled;
        input.Player.MoveToAd.canceled -= OnMovementCancelled;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveVector * moveSpeed;
    }

    private void OnMovementPerformed(InputAction.CallbackContext value)
    {
        moveVector = value.ReadValue<Vector2>();
    }

    private void OnMovementCancelled(InputAction.CallbackContext value)
    {
        moveVector = Vector2.zero;
    }
}
